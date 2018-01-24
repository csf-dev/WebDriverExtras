using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Providers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSF.WebDriverExtras.Factories
{
  public class ChromeProviderFactory : ICreatesWebDriverProvidersWithOptions
  {
    object ICreatesWebDriverProvidersWithOptions.CreateEmptyProviderOptions() => new LocalChromeOptions();

    IProvidesWebDriver ICreatesWebDriverProviders.CreateProvider(IDictionary<string,object> requestedCapabilities,
                                                                 IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, null, flagsProvider);

    IProvidesWebDriver ICreatesWebDriverProvidersWithOptions.CreateProvider(object options,
                                                                            IDictionary<string,object> requestedCapabilities,
                                                                            IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, (LocalChromeOptions) options, flagsProvider);

    public IProvidesWebDriver CreateProvider(IDictionary<string,object> requestedCapabilities,
                                             LocalChromeOptions options,
                                             IGetsBrowserFlags flagsProvider)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return GetProvider(webDriver, requestedCapabilities, options, flagsProvider);
    }

    IWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
                            LocalChromeOptions options)
    {
      var driverService = GetDriverService(options);
      var chromeOptions = GetChromeOptions(options);

      if(requestedCapabilities != null)
      {
        foreach(var cap in requestedCapabilities)
        {
          chromeOptions.AddAdditionalCapability(cap.Key, cap.Value);
        }
      }

      var timeout = (options?.GetCommandTimeout()).GetValueOrDefault(LocalProviderOptions.DefaultCommandTimeout);
      return new ChromeDriver(driverService, chromeOptions, timeout);

    }

    ChromeDriverService GetDriverService(LocalChromeOptions options)
    {
      ChromeDriverService output;

      if(String.IsNullOrEmpty(options?.WebDriverExecutablePath))
        output = ChromeDriverService.CreateDefaultService();
      else
        output = ChromeDriverService.CreateDefaultService(options.WebDriverExecutablePath);

      output.HideCommandPromptWindow = true;
      output.SuppressInitialDiagnosticInformation = true;

      if((options?.WebDriverPort).HasValue)
        output.Port = options.WebDriverPort.Value;

      return output;
    }

    ChromeOptions GetChromeOptions(LocalChromeOptions options)
    {
      var output = new ChromeOptions();

      if(!String.IsNullOrEmpty(options?.WebBrowserExecutablePath))
        output.BinaryLocation = options.WebBrowserExecutablePath;

      return output;
    }

    IProvidesWebDriver GetProvider(IWebDriver webDriver,
                                   IDictionary<string,object> requestedCapabilities,
                                   LocalChromeOptions options,
                                   IGetsBrowserFlags flagsProvider)
    {
      var baseProvider = GetProvider(webDriver, requestedCapabilities, options);

      if(flagsProvider == null)
        return baseProvider;

      var flags = flagsProvider.GetFlags(requestedCapabilities, options);
      return new WebDriverProviderWithFlags(baseProvider, flags);
    }

    WebDriverProvider GetProvider(IWebDriver webDriver,
                                  IDictionary<string,object> requestedCapabilities,
                                  LocalChromeOptions options)
    {
      return new WebDriverProvider(webDriver,
                                   BrowserName.GoogleChrome,
                                   options?.BrowserVersion,
                                   Environment.OSVersion.ToString(),
                                   requestedCapabilities);
    }
                                       
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CSF.WebDriverExtras.Factories
{
  public class FirefoxProviderFactory : ICreatesWebDriverProvidersWithOptions
  {
    object ICreatesWebDriverProvidersWithOptions.CreateEmptyProviderOptions() => new LocalFirefoxOptions();

    IProvidesWebDriver ICreatesWebDriverProviders.CreateProvider(IDictionary<string,object> requestedCapabilities,
                                                                 IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, null, flagsProvider);

    IProvidesWebDriver ICreatesWebDriverProvidersWithOptions.CreateProvider(object options,
                                                                            IDictionary<string,object> requestedCapabilities,
                                                                            IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, (LocalFirefoxOptions) options, flagsProvider);

    public IProvidesWebDriver CreateProvider(IDictionary<string,object> requestedCapabilities,
                                             LocalFirefoxOptions options,
                                             IGetsBrowserFlags flagsProvider)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return GetProvider(webDriver, requestedCapabilities, options, flagsProvider);
    }

    IWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
                            LocalFirefoxOptions options)
    {
      var driverService = GetDriverService(options);
      var firefoxOptions = GetFirefoxOptions(options);

      if(requestedCapabilities != null)
      {
        foreach(var cap in requestedCapabilities)
        {
          firefoxOptions.AddAdditionalCapability(cap.Key, cap.Value);
        }
      }

      var timeout = (options?.GetCommandTimeout()).GetValueOrDefault(LocalProviderOptions.DefaultCommandTimeout);
      return new FirefoxDriver(driverService, firefoxOptions, timeout);
    }

    FirefoxDriverService GetDriverService(LocalFirefoxOptions options)
    {
      FirefoxDriverService output;

      if(String.IsNullOrEmpty(options?.WebDriverExecutablePath))
        output = FirefoxDriverService.CreateDefaultService();
      else
        output = FirefoxDriverService.CreateDefaultService(options?.WebDriverExecutablePath);

      output.HideCommandPromptWindow = true;
      output.SuppressInitialDiagnosticInformation = true;

      if(options.WebDriverPort.HasValue)
        output.Port = options.WebDriverPort.Value;

      if(!String.IsNullOrEmpty(options.WebBrowserExecutablePath))
        output.FirefoxBinaryPath = options.WebBrowserExecutablePath;

      return output;
    }

    FirefoxOptions GetFirefoxOptions(LocalFirefoxOptions options)
    {
      var output = new FirefoxOptions();

      if(!String.IsNullOrEmpty(options.WebBrowserExecutablePath))
        output.BrowserExecutableLocation = options.WebBrowserExecutablePath;

      return output;
    }

    IProvidesWebDriver GetProvider(IWebDriver webDriver,
                                   IDictionary<string,object> requestedCapabilities,
                                   LocalFirefoxOptions options,
                                   IGetsBrowserFlags flagsProvider)
    {
      return new WebDriverProvider(webDriver,
                                   BrowserName.MozillaFirefox,
                                   options?.BrowserVersion,
                                   Environment.OSVersion.ToString(),
                                   requestedCapabilities,
                                   options,
                                   flagsProvider);
    }
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class ChromeDriverFactory : RemoteDriverFactoryBase<LocalChromeOptions>
  {
    public override IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               LocalChromeOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return CreateProxy(webDriver, flagsProvider);
    }

    RemoteWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
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

      var timeout = (options?.GetCommandTimeout()).GetValueOrDefault(LocalDriverOptions.DefaultCommandTimeout);
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
  }
}

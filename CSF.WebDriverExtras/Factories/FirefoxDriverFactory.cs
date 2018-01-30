using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class FirefoxDriverFactory : RemoteDriverFactoryBase<LocalFirefoxOptions>
  {
    public override IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               LocalFirefoxOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return CreateProxy(webDriver, flagsProvider);
    }

    RemoteWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
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

      var timeout = (options?.GetCommandTimeout()).GetValueOrDefault(LocalDriverOptions.DefaultCommandTimeout);
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
  }
}

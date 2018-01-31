using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Implementation of <see cref="T:RemoteDriverFactoryBase{TOptions}"/> for Google Chrome, using
  /// <see cref="LocalChromeOptions"/>
  /// </summary>
  public class ChromeDriverFactory : RemoteDriverFactoryBase<LocalChromeOptions>
  {
    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">A collection of requested web driver capabilities.</param>
    /// <param name="options">A factory options instance.</param>
    /// <param name="flagsProvider">A service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">The name for the current test scenario.</param>
    public override IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               LocalChromeOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return WrapWithProxy(webDriver, flagsProvider);
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

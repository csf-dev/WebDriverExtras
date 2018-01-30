using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class RemoteDriverFactory : RemoteDriverFactoryBase<RemoteDriverOptions>
  {
    public override IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               RemoteDriverOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return CreateProxy(webDriver, flagsProvider);
    }

    protected virtual RemoteWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
                                                   RemoteDriverOptions options)
    {
      var capabilities = new DesiredCapabilities();
      ConfigureCapabilities(capabilities, requestedCapabilities, options);

      var uri = options?.GetRemoteWebDriverEndpoint() ?? RemoteDriverOptions.DefaultRemoteWebDriverEndpoint;
      var timeout = options?.GetCommandTimeout() ?? RemoteDriverOptions.DefaultCommandTimeout;

      return new RemoteWebDriver(uri, capabilities, timeout);
    }

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureCapabilities(DesiredCapabilities caps,
                                                 IDictionary<string,object> requestedCapabilities,
                                                 RemoteDriverOptions options)
    {
      SetStandardCapabilities(caps, requestedCapabilities, options);
      SetExtraCapabilities(caps, requestedCapabilities);
    }

    protected virtual void SetStandardCapabilities(DesiredCapabilities caps,
                                                   IDictionary<string,object> requestedCapabilities,
                                                   RemoteDriverOptions options)
    {
      caps.SetCapability(CapabilityType.BrowserName, requestedCapabilities, options?.BrowserName);
      caps.SetOptionalCapability(CapabilityType.Version, requestedCapabilities, options?.BrowserVersion);
      caps.SetOptionalCapability(CapabilityType.Platform, requestedCapabilities, options?.Platform);
    }

    protected virtual void SetExtraCapabilities(DesiredCapabilities caps,
                                                IDictionary<string,object> requestedCapabilities)
    {
      if(requestedCapabilities == null)
        return;

      foreach(var capability in requestedCapabilities.Keys)
      {
        caps.SetCapability(capability, requestedCapabilities[capability]);
      }
    }
  }
}

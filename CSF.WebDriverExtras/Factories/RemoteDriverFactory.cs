using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Implementation of <see cref="T:RemoteDriverFactoryBase{TOptions}"/> for remote web drivers (those which are
  /// accessed via a remote API call), using <see cref="RemoteDriverOptions"/>
  /// </summary>
  public class RemoteDriverFactory : RemoteDriverFactoryBase<RemoteDriverOptions>
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
                                               RemoteDriverOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var desiredCapabilities = GetDesiredCapabilities(requestedCapabilities, options);
      var webDriver = GetWebDriver(desiredCapabilities, options);
      return WrapWithProxy(webDriver, flagsProvider, desiredCapabilities);
    }

    /// <summary>
    /// Gets the remote web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="capabilities">The desired capabilities.</param>
    /// <param name="options">Options.</param>
    protected virtual RemoteWebDriver GetWebDriver(ICapabilities capabilities,
                                                   RemoteDriverOptions options)
    {
      var uri = options?.GetRemoteWebDriverEndpoint() ?? RemoteDriverOptions.DefaultRemoteWebDriverEndpoint;
      var timeout = options?.GetCommandTimeout() ?? RemoteDriverOptions.DefaultCommandTimeout;

      return new RemoteWebDriver(uri, capabilities, timeout);
    }

    /// <summary>
    /// Gets an <c>ICapabilities</c> instance whcih represents the <see cref="RemoteDriverOptions"/> passed,
    /// combined with the <paramref name="requestedCapabilities"/>.
    /// </summary>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating capabiltiies explicitly requested by the calling code.</param>
    /// <param name="options">Options.</param>
    protected virtual ICapabilities GetDesiredCapabilities(IDictionary<string,object> requestedCapabilities,
                                                           RemoteDriverOptions options)
    {
      var caps = new DesiredCapabilities();
      SetStandardCapabilities(caps, requestedCapabilities, options);
      SetExtraCapabilities(caps, requestedCapabilities);
      return caps;
    }

    /// <summary>
    /// Sets up well-known capabilities which are expected to come from either the
    /// requested capabilities dictionary or from the options object.
    /// </summary>
    /// <param name="caps">A Selenium <c>DesiredCapabilities</c> instance.</param>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating capabilities passed to the
    /// <see cref="CreateWebDriver"/> method.</param>
    /// <param name="options">Options.</param>
    protected virtual void SetStandardCapabilities(DesiredCapabilities caps,
                                                   IDictionary<string,object> requestedCapabilities,
                                                   RemoteDriverOptions options)
    {
      caps.SetCapability(CapabilityType.BrowserName, requestedCapabilities, options?.BrowserName);
      caps.SetOptionalCapability(CapabilityType.Version, requestedCapabilities, options?.BrowserVersion);
      caps.SetOptionalCapability(CapabilityType.Platform, requestedCapabilities, options?.Platform);
    }

    /// <summary>
    /// Sets additional, arbitrary capabilities from the requested capabilities collection.
    /// </summary>
    /// <param name="caps">A Selenium <c>DesiredCapabilities</c> instance.</param>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating capabilities passed to the
    /// <see cref="CreateWebDriver"/> method.</param>
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

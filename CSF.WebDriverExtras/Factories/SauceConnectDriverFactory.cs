using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Proxies;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// A specialisation of <see cref="RemoteDriverFactory"/> for Sauce Labs' Sauce Connect web driver.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Sauce Labs documentation: https://wiki.saucelabs.com/display/DOCS/Sauce+Connect+Proxy
  /// </para>
  /// </remarks>
  public class SauceConnectDriverFactory : RemoteDriverFactory
  {
    const string
      TunnelIdCapabilityName = "tunnel-identifier",
      UsernameCapabilityName = "username",
      ApiKeyCapabilityName = "accessKey",
      BuildNameCapabilityName = "build",
      ScenarioNameCapabilityName = "name";

    /// <summary>
    /// Creates an empty instance of <see cref="RemoteDriverOptions"/>.
    /// </summary>
    /// <returns>The empty options.</returns>
    protected override RemoteDriverOptions CreateEmptyOptions() => new SauceConnectDriverOptions();

    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">A collection of requested web driver capabilities.</param>
    /// <param name="options">A factory options instance.</param>
    /// <param name="flagsProvider">A service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">The name for the current test scenario.</param>
    public override IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities,
                                               RemoteDriverOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var requestedCaps = requestedCapabilities ?? new Dictionary<string,object>();

      if(scenarioName != null)
        requestedCaps[ScenarioNameCapabilityName] = scenarioName;

      return base.CreateWebDriver(requestedCaps, options, flagsProvider, scenarioName);
    }

    /// <summary>
    /// Sets up well-known capabilities which are expected to come from either the
    /// requested capabilities dictionary or from the options object.
    /// </summary>
    /// <param name="caps">A Selenium <c>DesiredCapabilities</c> instance.</param>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating capabilities passed to the
    /// <see cref="M:CSF.WebDriverExtras.Factories.RemoteDriverFactory.CreateWebDriver(System.Collections.Generic.IDictionary{System.String,System.Object},CSF.WebDriverExtras.Factories.RemoteDriverOptions,CSF.WebDriverExtras.Flags.IGetsBrowserFlags,System.String)" /> method.</param>
    /// <param name="options">Options.</param>
    protected override void SetStandardCapabilities(DesiredCapabilities caps,
                                                    IDictionary<string, object> requestedCapabilities,
                                                    RemoteDriverOptions options)
    {
      base.SetStandardCapabilities(caps, requestedCapabilities, options);

      var sauceOptions = (SauceConnectDriverOptions) options;

      caps.SetOptionalCapability(TunnelIdCapabilityName, requestedCapabilities, sauceOptions?.TunnelIdentifier);
      caps.SetOptionalCapability(UsernameCapabilityName, requestedCapabilities, sauceOptions?.SauceConnectUsername);
      caps.SetOptionalCapability(ApiKeyCapabilityName, requestedCapabilities, sauceOptions?.SauceConnectApiKey);
      caps.SetOptionalCapability(BuildNameCapabilityName, requestedCapabilities, sauceOptions?.SauceLabsBuildName);
    }

    /// <summary>
    /// Gets a factory service which wraps remote web drivers in proxies.
    /// </summary>
    /// <returns>The proxy factory.</returns>
    protected override IWrapsRemoteDriversInProxies GetProxyFactory()
      => new SauceConnectWebDriverProxyCreator();
  }
}

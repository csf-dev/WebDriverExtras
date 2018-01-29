using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Proxies;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class SauceConnectDriverFactory : RemoteDriverFactory
  {
    const string
      TunnelIdCapabilityName = "tunnel-identifier",
      UsernameCapabilityName = "username",
      ApiKeyCapabilityName = "accessKey",
      BuildNameCapabilityName = "build",
      ScenarioNameCapabilityName = "name";

    protected override RemoteDriverOptions CreateEmptyOptions() => new SauceConnectDriverOptions();

    public override IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities,
                                               RemoteDriverOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName)
    {
      var caps = requestedCapabilities;

      if(scenarioName != null)
      {
        caps = caps ?? new Dictionary<string,object>();
        caps[ScenarioNameCapabilityName] = scenarioName;
      }

      return base.CreateWebDriver(requestedCapabilities, options, flagsProvider, scenarioName);
    }

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

    protected override IWrapsRemoteDriversInProxies GetProxyFactory()
      => new SauceConnectWebDriverProxyCreator();
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class SauceConnectFactory : RemoteProviderFactory
  {
    const string
      TunnelIdCapabilityName = "tunnel-identifier",
      UsernameCapabilityName = "username",
      ApiKeyCapabilityName = "accessKey",
      BuildNameCapabilityName = "build";

    protected override RemoteOptions CreateEmptyOptions() => new SauceConnectOptions();

    protected override void SetStandardCapabilities(DesiredCapabilities caps,
                                                    IDictionary<string, object> requestedCapabilities,
                                                    RemoteOptions options)
    {
      base.SetStandardCapabilities(caps, requestedCapabilities, options);

      var sauceOptions = (SauceConnectOptions) options;

      caps.SetOptionalCapability(TunnelIdCapabilityName, requestedCapabilities, sauceOptions?.TunnelIdentifier);
      caps.SetOptionalCapability(UsernameCapabilityName, requestedCapabilities, sauceOptions?.SauceConnectUsername);
      caps.SetOptionalCapability(ApiKeyCapabilityName, requestedCapabilities, sauceOptions?.SauceConnectApiKey);
      caps.SetOptionalCapability(BuildNameCapabilityName, requestedCapabilities, sauceOptions?.SauceLabsBuildName);
    }

    protected override IProvidesWebDriver GetProvider(IWebDriver webDriver,
                                                      IDictionary<string, object> requestedCapabilities,
                                                      RemoteOptions options,
                                                      IGetsBrowserFlags flagsProvider)
    {
      return new SauceConnectWebDriverProvider(webDriver,
                                               options?.BrowserName,
                                               options?.BrowserVersion,
                                               options?.Platform,
                                               requestedCapabilities,
                                               options,
                                               flagsProvider);
    }
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Providers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public class RemoteProviderFactory : ICreatesWebDriverProvidersWithOptions
  {
    object ICreatesWebDriverProvidersWithOptions.CreateEmptyProviderOptions() => new RemoteOptions();

    IProvidesWebDriver ICreatesWebDriverProviders.CreateProvider(IDictionary<string,object> requestedCapabilities,
                                                                 IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, null, flagsProvider);

    IProvidesWebDriver ICreatesWebDriverProvidersWithOptions.CreateProvider(object options,
                                                                            IDictionary<string,object> requestedCapabilities,
                                                                            IGetsBrowserFlags flagsProvider)
      => CreateProvider(requestedCapabilities, (RemoteOptions) options, flagsProvider);

    public IProvidesWebDriver CreateProvider(IDictionary<string,object> requestedCapabilities,
                                             RemoteOptions options,
                                             IGetsBrowserFlags flagsProvider)
    {
      var webDriver = GetWebDriver(requestedCapabilities, options);
      return GetProvider(webDriver, requestedCapabilities, options, flagsProvider);
    }

    protected virtual IWebDriver GetWebDriver(IDictionary<string,object> requestedCapabilities,
                                              RemoteOptions options)
    {
      var capabilities = new DesiredCapabilities();
      ConfigureCapabilities(capabilities, requestedCapabilities, options);

      var uri = options?.GetRemoteWebDriverEndpoint() ?? RemoteOptions.DefaultRemoteWebDriverEndpoint;
      var timeout = options?.GetCommandTimeout() ?? RemoteOptions.DefaultCommandTimeout;

      return new RemoteWebDriver(uri, capabilities, timeout);
    }

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureCapabilities(DesiredCapabilities caps,
                                                 IDictionary<string,object> requestedCapabilities,
                                                 RemoteOptions options)
    {
      caps.SetCapability(CapabilityType.BrowserName, requestedCapabilities, options?.BrowserName);
      caps.SetOptionalCapability(CapabilityType.Version, requestedCapabilities, options?.BrowserVersion);
      caps.SetOptionalCapability(CapabilityType.Platform, requestedCapabilities, options?.Platform);

      if(requestedCapabilities == null)
        return;

      foreach(var capability in requestedCapabilities.Keys)
      {
        caps.SetCapability(capability, requestedCapabilities[capability]);
      }
    }

    IProvidesWebDriver GetProvider(IWebDriver webDriver,
                                   IDictionary<string,object> requestedCapabilities,
                                   RemoteOptions options,
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
                                  RemoteOptions options)
    {
      return new WebDriverProvider(webDriver,
                                   options?.BrowserName,
                                   options?.BrowserVersion,
                                   options?.Platform,
                                   requestedCapabilities);
    }
  }
}

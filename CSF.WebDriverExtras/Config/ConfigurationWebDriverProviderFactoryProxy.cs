using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;

namespace CSF.WebDriverExtras.Config
{
  public class ConfigurationWebDriverProviderFactoryProxy : ICreatesWebDriverProviders
  {
    readonly object options;
    readonly ICreatesWebDriverProvidersWithOptions proxiedProvider;

    public IProvidesWebDriver CreateProvider(IDictionary<string, object> requestedCapabilities = null,
                                             IGetsBrowserFlags flagsProvider = null)
      => proxiedProvider.CreateProvider(this.options, requestedCapabilities, flagsProvider);

    public IProvidesWebDriver CreateProvider(object options,
                                             IDictionary<string, object> requestedCapabilities = null,
                                             IGetsBrowserFlags flagsProvider = null)
      => proxiedProvider.CreateProvider(options, requestedCapabilities, flagsProvider);

    public ConfigurationWebDriverProviderFactoryProxy(ICreatesWebDriverProvidersWithOptions proxiedProvider,
                                                      object options)
    {
      if(proxiedProvider == null)
        throw new ArgumentNullException(nameof(proxiedProvider));

      this.options = options;
      this.proxiedProvider = proxiedProvider;
    }
  }
}

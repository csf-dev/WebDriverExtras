using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class OptionsCachingProviderFactoryProxy : ICreatesWebDriverProviders
  {
    readonly object options;
    readonly ICreatesWebDriverProvidersWithOptions proxiedProvider;

    public ICreatesWebDriverProviders ProxiedProvider => proxiedProvider;

    public IProvidesWebDriver CreateProvider(IDictionary<string, object> requestedCapabilities = null,
                                             IGetsBrowserFlags flagsProvider = null)
      => proxiedProvider.CreateProvider(this.options, requestedCapabilities, flagsProvider);

    public OptionsCachingProviderFactoryProxy(ICreatesWebDriverProvidersWithOptions proxiedProvider,
                                                      object options)
    {
      if(proxiedProvider == null)
        throw new ArgumentNullException(nameof(proxiedProvider));

      this.options = options;
      this.proxiedProvider = proxiedProvider;
    }
  }
}

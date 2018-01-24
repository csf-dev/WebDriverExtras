using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;

namespace CSF.WebDriverExtras.Factories
{
  public interface ICreatesWebDriverProvidersWithOptions : ICreatesWebDriverProviders
  {
    object CreateEmptyProviderOptions();

    IProvidesWebDriver CreateProvider(object options,
                                      IDictionary<string,object> requestedCapabilities = null,
                                      IGetsBrowserFlags flagsProvider = null);
    
  }
}

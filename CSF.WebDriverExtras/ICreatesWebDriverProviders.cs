using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;

namespace CSF.WebDriverExtras
{
  public interface ICreatesWebDriverProviders
  {
    IProvidesWebDriver CreateProvider(IDictionary<string,object> requestedCapabilities = null,
                                      IGetsBrowserFlags flagsProvider = null);
  }
}

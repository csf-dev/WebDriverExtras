using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras
{
  public interface IHasRequestedCapabilities
  {
    bool HasRequestedCapability(string capability);

    IReadOnlyDictionary<string,object> RequestedCapabilities { get; }
  }
}

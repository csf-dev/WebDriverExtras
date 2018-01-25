using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Flags
{
  public interface IGetsBrowserFlags
  {
    IReadOnlyCollection<string> GetFlags(IDictionary<string, object> requestedCapabilities, object providerOptions);
  }
}

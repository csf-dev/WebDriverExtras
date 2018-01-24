using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Flags
{
  public interface IGetsBrowserFlags
  {
    ICollection<string> GetFlags(IDictionary<string, object> requestedCapabilities, object providerOptions);
  }
}

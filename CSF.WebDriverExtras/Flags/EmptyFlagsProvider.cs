using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Flags
{
  public class EmptyFlagsProvider : IGetsBrowserFlags
  {
    static IReadOnlyCollection<string> Empty => new string[0];

    public IReadOnlyCollection<string> GetFlags(IDictionary<string, object> requestedCapabilities,
                                                object providerOptions) => Empty;
  }
}

using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras
{
  public interface IHasFlags
  {
    IReadOnlyCollection<string> GetFlags();

    bool HasFlag(string flag);

    string GetFirstFlagPresent(params string[] flags);

    string GetFirstFlagPresent(IList<string> flags);
  }
}

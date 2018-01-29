using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras
{
  public interface IHasFlags
  {
    IReadOnlyCollection<string> GetFlags();

    bool HasFlag(string flag);
  }
}

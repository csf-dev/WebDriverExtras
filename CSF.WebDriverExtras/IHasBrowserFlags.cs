using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras
{
  public interface IHasBrowserFlags : IProvidesWebDriver
  {
    bool HasFlag(string flag);

    IReadOnlyCollection<string> Flags { get; }
  }
}

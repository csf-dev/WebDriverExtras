using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  public class EmptyFlagsProvider : IGetsBrowserFlags
  {
    static IReadOnlyCollection<string> Empty => new string[0];

    public IReadOnlyCollection<string> GetFlags(IHasCapabilities webDriver) => Empty;
  }
}

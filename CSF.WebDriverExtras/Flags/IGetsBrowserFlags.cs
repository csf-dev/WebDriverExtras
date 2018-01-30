using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  public interface IGetsBrowserFlags
  {
    IReadOnlyCollection<string> GetFlags(IHasCapabilities webDriver);
  }
}

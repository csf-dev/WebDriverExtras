using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  public interface IGetsBrowserIdentification
  {
    BrowserIdentification GetIdentification(IHasCapabilities webDriver);
  }
}

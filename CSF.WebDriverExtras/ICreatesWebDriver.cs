using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  public interface ICreatesWebDriver
  {
    IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities = null,
                               IGetsBrowserFlags flagsProvider = null,
                               string scenarioName = null);
  }
}

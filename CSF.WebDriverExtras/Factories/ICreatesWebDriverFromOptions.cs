using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Factories
{
  public interface ICreatesWebDriverFromOptions : ICreatesWebDriver
  {
    object CreateEmptyOptions();

    IWebDriver CreateWebDriver(object options,
                               IDictionary<string,object> requestedCapabilities = null,
                               IGetsBrowserFlags flagsProvider = null,
                               string scenarioName = null);
    
  }
}

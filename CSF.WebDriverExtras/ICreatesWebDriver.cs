using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  /// <summary>
  /// A factory which creates web driver instances.
  /// </summary>
  public interface ICreatesWebDriver
  {
    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">An optional collection of requested web driver capabilities.</param>
    /// <param name="flagsProvider">An optional service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">An optional name for the current test scenario.</param>
    IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities = null,
                               IGetsBrowserFlags flagsProvider = null,
                               string scenarioName = null);
  }
}

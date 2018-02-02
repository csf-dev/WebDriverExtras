using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// A specialisation of <see cref="ICreatesWebDriver"/> which is able to pass 'factory options'
  /// when creating a web driver.  It is additionally able to generate empty instances of those factory options.
  /// </summary>
  public interface ICreatesWebDriverFromOptions : ICreatesWebDriver
  {
    /// <summary>
    /// Creates an empty instance of a factory options type which would be suitable for use with this factory's
    /// <see cref="CreateWebDriver"/> method.
    /// </summary>
    /// <returns>The empty options.</returns>
    object CreateEmptyOptions();

    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">A collection of requested web driver capabilities.</param>
    /// <param name="options">A factory options instance.</param>
    /// <param name="flagsProvider">A service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">The name for the current test scenario.</param>
    IWebDriver CreateWebDriver(object options,
                               IDictionary<string,object> requestedCapabilities = null,
                               IGetsBrowserFlags flagsProvider = null,
                               string scenarioName = null);
    
  }
}

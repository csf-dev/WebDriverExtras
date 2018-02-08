using System;
using CSF.WebDriverExtras.BrowserId;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  /// <summary>
  /// Extension methods for Web Drivers.
  /// </summary>
  public static class WebDriverExtensions
  {
    static readonly IGetsBrowserIdentification browserIdFactory;

    /// <summary>
    /// Gets identification information about the given web driver.
    /// </summary>
    /// <returns>The browser identification.</returns>
    /// <param name="webDriver">Web driver.</param>
    public static BrowserIdentification GetIdentification(this IWebDriver webDriver)
      => browserIdFactory.GetIdentification(webDriver as IHasCapabilities);

    /// <summary>
    /// Initializes the <see cref="T:CSF.WebDriverExtras.WebDriverExtensions"/> class.
    /// </summary>
    static WebDriverExtensions()
    {
      browserIdFactory = new BrowserIdentificationFactory();
    }
  }
}

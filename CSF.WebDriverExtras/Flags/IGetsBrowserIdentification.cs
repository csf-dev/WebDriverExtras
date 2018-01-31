using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// A factory which creates <see cref="BrowserIdentification"/> instances from web drivers.
  /// </summary>
  public interface IGetsBrowserIdentification
  {
    /// <summary>
    /// Gets a browser identification instance from the given web driver.
    /// </summary>
    /// <returns>The identification.</returns>
    /// <param name="webDriver">Web driver.</param>
    BrowserIdentification GetIdentification(IHasCapabilities webDriver);
  }
}

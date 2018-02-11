using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.BrowserId
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
    /// <param name="webDriver">A web drivr which has capabilities.</param>
    BrowserIdentification GetIdentification(IHasCapabilities webDriver);

    /// <summary>
    /// Gets a browser identification instance from the given web driver.
    /// </summary>
    /// <returns>The identification.</returns>
    /// <param name="webDriver">A web drivr which has capabilities.</param>
    /// <param name="desiredCapabilities">The originally-requested capabilities.</param>
    BrowserIdentification GetIdentification(IHasCapabilities webDriver, ICapabilities desiredCapabilities);
  }
}

using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  /// <summary>
  /// A gateway service which sends test scenario success/failure information to a web driver.
  /// </summary>
  public interface ISuccessAndFailureGateway
  {
    /// <summary>
    /// Sends a 'test scenario outcome' to a web driver (either success or failure).
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    /// <param name="outcome">If set to <c>true</c> then success is sent; otherwise failure.</param>
    void SendOutcome(IWebDriver webDriver, bool outcome);

    /// <summary>
    /// Sends a 'test scenario success' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    void SendSuccess(IWebDriver webDriver);

    /// <summary>
    /// Sends a 'test scenario failure' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    void SendFailure(IWebDriver webDriver);
  }
}

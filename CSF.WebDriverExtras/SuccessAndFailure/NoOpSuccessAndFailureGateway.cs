using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  /// <summary>
  /// A no-op implementaion of <see cref="ISuccessAndFailureGateway"/>.
  /// </summary>
  public class NoOpSuccessAndFailureGateway : ISuccessAndFailureGateway
  {
    /// <summary>
    /// Sends a 'test scenario outcome' to a web driver (either success or failure).
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    /// <param name="outcome">If set to <c>true</c> then success is sent; otherwise failure.</param>
    public void SendOutcome(IWebDriver webDriver, bool outcome)  { /* Intentional no-op */ }

    /// <summary>
    /// Sends a 'test scenario failure' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    public void SendFailure(IWebDriver webDriver) { /* Intentional no-op */ }

    /// <summary>
    /// Sends a 'test scenario success' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    public void SendSuccess(IWebDriver webDriver) { /* Intentional no-op */ }
  }
}

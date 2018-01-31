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

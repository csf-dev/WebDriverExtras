using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  /// <summary>
  /// An implementation of <see cref="ISuccessAndFailureGateway"/> which is configured for
  /// Sauce Labs' Sauce Connect web driver API.
  /// </summary>
  public class SauceLabsSuccessFailureGateway : ISuccessAndFailureGateway
  {
    const string SendOutcomeJavascriptTemplate = "sauce:job-result={0}";

    /// <summary>
    /// Sends a 'test scenario outcome' to a web driver (either success or failure).
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    /// <param name="outcome">If set to <c>true</c> then success is sent; otherwise failure.</param>
    public void SendOutcome(IWebDriver webDriver, bool outcome)
    {
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));

      var javascriptExecutor = webDriver as IJavaScriptExecutor;
      if(javascriptExecutor == null)
        return;

      var stringOutcome = outcome.ToString().ToLowerInvariant();
      var outcomeScript = String.Format(SendOutcomeJavascriptTemplate, stringOutcome);

      javascriptExecutor.ExecuteScript(outcomeScript);
    }

    /// <summary>
    /// Sends a 'test scenario success' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    public void SendSuccess(IWebDriver webDriver) => SendOutcome(webDriver, true);

    /// <summary>
    /// Sends a 'test scenario failure' to a web driver.
    /// </summary>
    /// <param name="webDriver">Web driver.</param>
    public void SendFailure(IWebDriver webDriver) => SendOutcome(webDriver, false);
  }
}

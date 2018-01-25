using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  public class SauceLabsSuccessFailureGateway : ISuccessAndFailureGateway
  {
    const string SendOutcomeJavascriptTemplate = "sauce:job-result={0}";

    public void SendSuccess(IWebDriver webDriver) => SendOutcome(webDriver, true);

    public void SendFailure(IWebDriver webDriver) => SendOutcome(webDriver, false);

    void SendOutcome(IWebDriver webDriver, bool outcome)
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
  }
}

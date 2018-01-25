using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  public class NoOpSuccessAndFailureGateway : ISuccessAndFailureGateway
  {
    public void SendFailure(IWebDriver webDriver) { /* Intentional no-op */ }

    public void SendSuccess(IWebDriver webDriver) { /* Intentional no-op */ }
  }
}

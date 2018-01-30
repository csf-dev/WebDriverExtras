using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.SuccessAndFailure
{
  public interface ISuccessAndFailureGateway
  {
    void SendSuccess(IWebDriver webDriver);

    void SendFailure(IWebDriver webDriver);
  }
}

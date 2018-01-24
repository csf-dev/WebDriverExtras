using System;
namespace CSF.WebDriverExtras
{
  public interface ICanSendSuccessFailureInfo : IProvidesWebDriver
  {
    void SendSuccess();

    void SendFailure();
  }
}

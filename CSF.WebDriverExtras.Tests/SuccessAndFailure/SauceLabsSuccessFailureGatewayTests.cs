using System;
using CSF.WebDriverExtras.SuccessAndFailure;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Tests.SuccessAndFailure
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class SauceLabsSuccessFailureGatewayTests
  {
    [Test,AutoMoqData]
    public void SendSuccess_executes_success_script([ExecutesScript] IWebDriver driver,
                                                    SauceLabsSuccessFailureGateway sut)
    {
      // Act
      sut.SendSuccess(driver);

      // Assert
      Mock.Get(driver)
          .As<IJavaScriptExecutor>()
          .Verify(x => x.ExecuteScript("sauce:job-result=true"), Times.Once);
    }

    [Test,AutoMoqData]
    public void SendFailure_executes_failure_script([ExecutesScript] IWebDriver driver,
                                                    SauceLabsSuccessFailureGateway sut)
    {
      // Act
      sut.SendFailure(driver);

      // Assert
      Mock.Get(driver)
          .As<IJavaScriptExecutor>()
          .Verify(x => x.ExecuteScript("sauce:job-result=false"), Times.Once);
    }
  }
}

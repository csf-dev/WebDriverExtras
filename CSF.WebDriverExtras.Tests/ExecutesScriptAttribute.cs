using System;
using System.Reflection;
using Moq;
using OpenQA.Selenium;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests
{
  public class ExecutesScriptAttribute : CustomizeAttribute
  {
    public override ICustomization GetCustomization(ParameterInfo parameter) => new ExecutesScriptCustomisation();

    class ExecutesScriptCustomisation : ICustomization
    {
      public void Customize(IFixture fixture)
      {
        fixture.Customize<IWebDriver>(c => {
          return c
            .FromFactory(() => {
              var output = new Mock<IWebDriver>();
              output.As<IJavaScriptExecutor>();
              return output.Object;
            })
            .OmitAutoProperties();
        });
      }
    }
  }
}

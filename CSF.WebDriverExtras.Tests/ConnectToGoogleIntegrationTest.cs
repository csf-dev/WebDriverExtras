using System;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace CSF.WebDriverExtras.Tests
{
  [TestFixture,Explicit,Category("Browser"),Description("Connect a web driver to Google")]
  public class ConnectToGoogleIntegrationTest
  {
    [Test,Description("The web driver can read the title from the Google home page")]
    public void Can_read_Google_window_title()
    {
      // Arrange
      var scenarioName = "Connect a web driver to Google: The web driver can read the title from the Google home page";
      var driverFactory = GetWebDriverFactory.FromConfiguration();
      var webDriver = driverFactory.CreateWebDriver(scenarioName: scenarioName);

      // Act
      webDriver.Navigate().GoToUrl("https://google.com/");
      var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(2))
        .Until(ExpectedConditions.TitleContains("Google"));
      var result = webDriver.Title;

      // Assert
      Assert.That(result, Contains.Substring("Google"));
    }
  }
}

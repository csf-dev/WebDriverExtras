using System;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace CSF.WebDriverExtras.Tests
{
  [TestFixture]
  [Explicit]
  [Category("Browser")]
  [Description("End-to-end integration tests which use GetWebDriverFactory.FromConfiguration() and then make use of the driver to do something")]
  public class WebDriverFactoryFromConfigurationIntegrationTests
  {
    const string
      ConnectToGoogleScenarioName = "It should be possible to create a web driver from configuration, then use that driver to connect to the Google home page and read the window title",
      GoogleHomeUrl = "https://google.com/",
      Google = "Google";
    const int TimeoutSecondsToConnectToGoogleHomePage = 2;

    [Test]
    [Description(ConnectToGoogleScenarioName)]
    public void Can_create_webdriver_then_connect_to_Google_and_read_the_window_title()
    {
      // Arrange
      var driverFactory = GetWebDriverFactory.FromConfiguration();
      string result;

      // Act
      using(var webDriver = driverFactory.CreateWebDriver(scenarioName: ConnectToGoogleScenarioName))
      {
        webDriver.Navigate().GoToUrl(GoogleHomeUrl);
        var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(TimeoutSecondsToConnectToGoogleHomePage))
          .Until(ExpectedConditions.TitleContains(Google));
        
        result = webDriver.Title;

        // Assert
        var statusDriver = webDriver as ICanReceiveScenarioOutcome;

        try { Assert.That(result, Contains.Substring(Google)); }
        catch(AssertionException)
        {
          if(statusDriver != null) statusDriver.MarkScenarioAsFailure();
          throw;
        }

        if(statusDriver != null) statusDriver.MarkScenarioAsSuccess();
      }
    }
  }
}

using System;
using CSF.WebDriverExtras;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;
using OpenQA.Selenium;
using Moq;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class BrowserIdentificationFactoryTests
  {
    [Test,AutoMoqData]
    public void GetIdentification_integration_test_can_create_identification_for_a_browser(IHasCapabilities driver,
                                                                                           ICapabilities caps)
    {
      // Arrange
      var versionString = "FlamboyantBannana";
      var platformType = PlatformType.Android;
      var browserName = "FooBrowser";

      var sut = new BrowserIdentificationFactory();
      Mock.Get(driver).SetupGet(x => x.Capabilities).Returns(caps);
      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(browserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(versionString);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(platformType));

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_browser_name(IHasCapabilities driver,
                                                                             ICapabilities caps)
    {
      // Arrange
      var versionString = "FlamboyantBannana";
      var platformType = PlatformType.Android;
      var browserName = "FooBrowser";

      var sut = new BrowserIdentificationFactory();
      Mock.Get(driver).SetupGet(x => x.Capabilities).Returns(caps);
      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(browserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(versionString);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(platformType));

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Name, Is.EqualTo(browserName));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_unrecognised_version(IHasCapabilities driver,
                                                                                     ICapabilities caps)
    {
      // Arrange
      var versionString = "FlamboyantBannana";
      var platformType = PlatformType.Android;
      var browserName = "FooBrowser";

      var sut = new BrowserIdentificationFactory();
      Mock.Get(driver).SetupGet(x => x.Capabilities).Returns(caps);
      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(browserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(versionString);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(platformType));

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Version, Is.EqualTo(new UnrecognisedVersion(versionString)));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_semantic_version(IHasCapabilities driver,
                                                                                 ICapabilities caps)
    {
      // Arrange
      var versionString = "v1.2.3";
      var platformType = PlatformType.Android;
      var browserName = "FooBrowser";

      var sut = new BrowserIdentificationFactory();
      Mock.Get(driver).SetupGet(x => x.Capabilities).Returns(caps);
      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(browserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(versionString);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(platformType));

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Version, Is.EqualTo(SemanticVersion.Parse(versionString)));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_platform_name(IHasCapabilities driver,
                                                                              ICapabilities caps)
    {
      // Arrange
      var versionString = "FlamboyantBannana";
      var platformType = PlatformType.Android;
      var browserName = "FooBrowser";

      var sut = new BrowserIdentificationFactory();
      Mock.Get(driver).SetupGet(x => x.Capabilities).Returns(caps);
      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(browserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(versionString);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(platformType));

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Platform, Is.EqualTo(platformType.ToString()));
    }

    /// <summary>
    /// Integration test which uses a real Web Driver to verify that every supported web driver may be identified.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This test method (and the many scenarios it executes) connects to a Remote Web Driver (usually Sauce Labs)
    /// and spins up a browser instance, then gets its identification, sends the browser a single simple command
    /// and then closes the web driver.
    /// </para>
    /// <para>
    /// The single command sent is required, because Sauce Labs (but perhaps other remote web driver providers) counts
    /// the test as an error if no commands are sent.  The command itself is not important.
    /// </para>
    /// <para>
    /// In regular operation this test is not required though.  The things that it verifies are covered by the test
    /// <see cref="VersionFactoryTests.CreateVersion_can_create_versions_for_all_supported_browsers"/>, because
    /// the information from this test has been collected and compiled into this assembly.
    /// </para>
    /// <para>
    /// The only reason for wanting to run THIS scenario again would be to detect newly-released browser versions
    /// (and this should be done periodically, but not on every build).
    /// </para>
    /// </remarks>
    /// <param name="platform">Platform.</param>
    /// <param name="browserName">Browser name.</param>
    /// <param name="browserVersion">Browser version.</param>
    [NonParallelizable]
    [Category("Browser")]
    [Explicit("This can only be executed with configuration for a remote web browser, and often isn't neccesary.  See the comments on the test for more info.")]
    [TestCaseSource(typeof(SupportedBrowserConfigurations), nameof(SupportedBrowserConfigurations.AsTestCaseData))]
    public void GetIdentification_integration_successfully_identifies_supported_web_drivers(string platform,
                                                                                            string browserName,
                                                                                            string browserVersion)
    {
      // Arrange
      var scenarioName = nameof(GetIdentification_integration_successfully_identifies_supported_web_drivers);
      var caps = GetCapabilities(platform, browserName, browserVersion);
      var factory = GetWebDriverFactory.FromConfiguration();

      BrowserIdentification result;

      // Act
      using(var webDriver = factory.CreateWebDriver(caps, scenarioName: scenarioName))
      {
        result = webDriver.GetIdentification();
        webDriver.Navigate().GoToUrl("http://google.com/");

        SendScenarioStatus(webDriver, !(result.Version is UnrecognisedVersion));
      }

      // Assert
      Assert.That(result.Version,
                  Is.Not.InstanceOf<UnrecognisedVersion>(),
                  "Browser was not recognised:{0}",
                  result);
    }

    IDictionary<string,object> GetCapabilities(string platform,
                                               string browserName,
                                               string browserVersion)
    {
      if(platform == null)
        throw new ArgumentNullException(nameof(platform));
      if(browserName == null)
        throw new ArgumentNullException(nameof(browserName));

      var caps = new Dictionary<string,object>();

      caps.Add(CapabilityType.Platform, platform);
      caps.Add(CapabilityType.BrowserName, browserName);

      if(!String.IsNullOrEmpty(browserVersion))
        caps.Add(CapabilityType.Version, browserVersion);
      
      return caps;
    }

    void SendScenarioStatus(IWebDriver driver, bool isSuccess)
    {
      if(driver is ICanReceiveScenarioStatus)
      {
        var statusDriver = (ICanReceiveScenarioStatus) driver;

        if(isSuccess)
          statusDriver.MarkScenarioAsSuccess();
        else
          statusDriver.MarkScenarioAsFailure();
      }
    }
  }
}

using System;
using CSF.WebDriverExtras;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;
using OpenQA.Selenium;
using Moq;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;
using CSF.WebDriverExtras.Tests.Autofixture;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class BrowserIdentificationFactoryTests
  {
    [Test,AutoMoqData]
    public void GetIdentification_uses_version_creator(ICreatesBrowserVersions versionFactory,
                                                       [BrowserId] IHasCapabilities driver,
                                                       SimpleStringVersion expectedVersion)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory(versionFactory);
      Mock.Get(versionFactory)
          .Setup(x => x.CreateVersion(driver.Capabilities.Version, driver.Capabilities.BrowserName, null))
          .Returns(expectedVersion);

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Version, Is.SameAs(expectedVersion));
    }

    [Test,AutoMoqData]
    public void GetIdentification_passes_original_browser_version_to_version_factory_when_it_exists(ICreatesBrowserVersions versionFactory,
                                                                                                    [BrowserId(RequestedVersion = "123")] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory(versionFactory);

      // Act
      sut.GetIdentification(driver);

      // Assert
      Mock.Get(versionFactory)
          .Verify(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>(), "123"), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetIdentification_can_pass_null_to_version_factory_as_original_version(ICreatesBrowserVersions versionFactory,
                                                                                       [BrowserId(HasRequestedVersion = true)] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory(versionFactory);

      // Act
      sut.GetIdentification(driver);

      // Assert
      Mock.Get(versionFactory)
          .Verify(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>(), null), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetIdentification_passes_explicit_requested_version_to_factory(ICreatesBrowserVersions versionFactory,
                                                                               [BrowserId] IHasCapabilities driver,
                                                                               ICapabilities requestedCaps,
                                                                               string requestedVersion)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory(versionFactory);
      Mock.Get(requestedCaps).SetupGet(x => x.Version).Returns(requestedVersion);

      // Act
      sut.GetIdentification(driver, requestedCaps);

      // Assert
      Mock.Get(versionFactory)
          .Verify(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>(), requestedVersion), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetIdentification_uses_explicit_requested_version_over_version_stored_in_driver(ICreatesBrowserVersions versionFactory,
                                                                                                [BrowserId(RequestedVersion = "123")] IHasCapabilities driver,
                                                                                                ICapabilities requestedCaps)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory(versionFactory);
      Mock.Get(requestedCaps).SetupGet(x => x.Version).Returns("456");

      // Act
      sut.GetIdentification(driver, requestedCaps);

      // Assert
      Mock.Get(versionFactory)
          .Verify(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>(), "456"), Times.Once);
      Mock.Get(versionFactory)
          .Verify(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>(), "123"), Times.Never);
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_can_create_identification_for_a_browser([BrowserId] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory();

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result, Is.Not.Null);
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_browser_name([BrowserId(Browser = "FooBrowser")] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory();

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Name, Is.EqualTo("FooBrowser"));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_unrecognised_version([BrowserId(Version = "FlamboyantBannana")] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory();

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Version, Is.EqualTo(new UnrecognisedVersion("FlamboyantBannana")));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_semantic_version([BrowserId(Version = "v1.2.3")] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory();

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Version, Is.EqualTo(SemanticVersion.Parse("v1.2.3")));
    }

    [Test,AutoMoqData]
    public void GetIdentification_integration_test_gets_correct_platform_name([BrowserId(Platform = PlatformType.Android)] IHasCapabilities driver)
    {
      // Arrange
      var sut = new BrowserIdentificationFactory();

      // Act
      var result = sut.GetIdentification(driver);

      // Assert
      Assert.That(result.Platform, Is.EqualTo(PlatformType.Android.ToString()));
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

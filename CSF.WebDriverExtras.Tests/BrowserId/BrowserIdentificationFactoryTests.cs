using System;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;
using OpenQA.Selenium;
using Moq;

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
  }
}

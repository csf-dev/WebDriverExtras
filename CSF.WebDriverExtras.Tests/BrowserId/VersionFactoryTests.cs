using System;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class VersionFactoryTests
  {
    [Test,AutoMoqData]
    public void CreateVersion_can_create_a_semantic_version(VersionFactory sut)
    {
      // Arrange
      var versionString = "v1.2.3";

      // Act
      var result = sut.CreateVersion(versionString);

      // Assert
      Assert.That(result, Is.EqualTo(SemanticVersion.Parse(versionString)));
    }

    [Test,AutoMoqData]
    public void CreateVersion_can_create_an_unrecognised_version(VersionFactory sut)
    {
      // Arrange
      var versionString = "version 55.20";

      // Act
      var result = sut.CreateVersion(versionString);

      // Assert
      Assert.That(result, Is.EqualTo(new UnrecognisedVersion(versionString)));
    }

    [TestCaseSource(typeof(SupportedBrowserConfigurations), nameof(SupportedBrowserConfigurations.ActualIdentifierTestCaseData))]
    public void CreateVersion_can_create_versions_for_all_supported_browsers(string browserName, string browserVersion)
    {
      // Arrange
      var sut = new VersionFactory();

      // Act
      var result = sut.CreateVersion(browserVersion, browserName);

      // Assert
      Assert.That(result,
                  Is.Not.InstanceOf<UnrecognisedVersion>(),
                  $"{nameof(VersionFactory)} created an unrecognised version for {browserName}: '{browserVersion}'.");
    }
  }
}

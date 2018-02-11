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

    [Test,AutoMoqData]
    public void CreateVersion_can_create_an_empty_version_from_empty_string(VersionFactory sut)
    {
      // Act
      var result = sut.CreateVersion(String.Empty);

      // Assert
      Assert.That(result, Is.EqualTo(EmptyBrowserVersion.Singleton));
    }

    [Test,AutoMoqData]
    public void CreateVersion_can_create_an_empty_version_from_a_null_string(VersionFactory sut)
    {
      // Act
      var result = sut.CreateVersion(null);

      // Assert
      Assert.That(result, Is.EqualTo(EmptyBrowserVersion.Singleton));
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

    [Test,AutoMoqData]
    public void CreateVersion_can_create_a_presumed_version_from_a_null_input_version(VersionFactory sut)
    {
      // Arrange
      var presumedVersion = "v1.2.3";

      // Act
      var result = sut.CreateVersion(null, requestedVersionString: presumedVersion);

      // Assert
      Assert.That(result, Is.EqualTo(SemanticVersion.Parse(presumedVersion)));
    }

    [Test,AutoMoqData]
    public void CreateVersion_can_create_a_presumed_version_from_an_empty_input_version(VersionFactory sut)
    {
      // Arrange
      var presumedVersion = "v1.2.3";

      // Act
      var result = sut.CreateVersion(String.Empty, requestedVersionString: presumedVersion);

      // Assert
      Assert.That(result, Is.EqualTo(SemanticVersion.Parse(presumedVersion)));
    }

    [Test,AutoMoqData]
    public void CreateVersion_marks_a_presumed_version_from_a_null_input_version_accordingly(VersionFactory sut)
    {
      // Arrange
      var presumedVersion = "v1.2.3";

      // Act
      var result = sut.CreateVersion(null, requestedVersionString: presumedVersion);

      // Assert
      Assert.That(result.IsPresumedVersion, Is.True);
    }

    [Test,AutoMoqData]
    public void CreateVersion_marks_a_presumed_version_from_an_empty_input_version_accordingly(VersionFactory sut)
    {
      // Arrange
      var presumedVersion = "v1.2.3";

      // Act
      var result = sut.CreateVersion(String.Empty, requestedVersionString: presumedVersion);

      // Assert
      Assert.That(result.IsPresumedVersion, Is.True);
    }

    [Test,AutoMoqData]
    public void CreateVersion_does_not_create_presumed_version_when_input_version_is_not_null_or_empty(VersionFactory sut,
                                                                                                       string versionstring,
                                                                                                       string presumedVersion)
    {
      // Act
      var result = sut.CreateVersion(versionstring, requestedVersionString: presumedVersion);

      // Assert
      Assert.That(result.IsPresumedVersion, Is.False);
    }
  }
}

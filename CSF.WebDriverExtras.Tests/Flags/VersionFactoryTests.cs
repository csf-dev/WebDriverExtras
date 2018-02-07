using System;
using CSF.WebDriverExtras.Flags;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.Flags
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
      var versionString = "55.20";

      // Act
      var result = sut.CreateVersion(versionString);

      // Assert
      Assert.That(result, Is.EqualTo(new UnrecognisedVersion(versionString)));
    }
  }
}

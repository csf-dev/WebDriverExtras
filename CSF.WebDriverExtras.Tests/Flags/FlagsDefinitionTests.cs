using System;
using NUnit.Framework;
using CSF.WebDriverExtras.Flags;

namespace CSF.WebDriverExtras.Tests.Flags
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class FlagsDefinitionTests
  {
    [Test,AutoMoqData]
    public void Matches_returns_true_when_all_aspects_are_present_and_match(string browserName, string platform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("b"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("a"),
        MaximumVersion = new SimpleStringVersion("c"),
      };
      sut.BrowserNames.Add(browserName);
      sut.Platforms.Add(platform);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.True);
    }

    [Test,AutoMoqData]
    public void Matches_returns_true_when_all_aspects_match_but_platform_is_missing(string browserName, string platform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("b"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("a"),
        MaximumVersion = new SimpleStringVersion("c"),
      };
      sut.BrowserNames.Add(browserName);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.True);
    }

    [Test,AutoMoqData]
    public void Matches_returns_true_when_all_aspects_match_but_platform_and_versions_are_missing(string browserName,
                                                                                                  string platform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("b"), platform);

      var sut = new FlagsDefinition();
      sut.BrowserNames.Add(browserName);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.True);
    }

    [Test,AutoMoqData]
    public void Matches_returns_false_when_platform_differs(string browserName, string platform, string otherPlatform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("b"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("a"),
        MaximumVersion = new SimpleStringVersion("c"),
      };
      sut.BrowserNames.Add(browserName);
      sut.Platforms.Add(otherPlatform);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.False);
    }

    [Test,AutoMoqData]
    public void Matches_returns_false_when_browser_name_differs(string browserName, string platform, string otherBrowser)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("b"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("a"),
        MaximumVersion = new SimpleStringVersion("c"),
      };
      sut.BrowserNames.Add(otherBrowser);
      sut.Platforms.Add(platform);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.False);
    }

    [Test,AutoMoqData]
    public void Matches_returns_false_when_version_is_before_min(string browserName, string platform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("a"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("b"),
        MaximumVersion = new SimpleStringVersion("d"),
      };
      sut.BrowserNames.Add(browserName);
      sut.Platforms.Add(platform);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.False);
    }

    [Test,AutoMoqData]
    public void Matches_returns_false_when_version_is_after_max(string browserName, string platform)
    {
      // Arrange
      var browserId = new BrowserIdentification(browserName, new SimpleStringVersion("e"), platform);

      var sut = new FlagsDefinition {
        MinimumVersion = new SimpleStringVersion("b"),
        MaximumVersion = new SimpleStringVersion("d"),
      };
      sut.BrowserNames.Add(browserName);
      sut.Platforms.Add(platform);

      // Act
      var result = sut.Matches(browserId);

      // Assert
      Assert.That(result, Is.False);
    }
  }
}

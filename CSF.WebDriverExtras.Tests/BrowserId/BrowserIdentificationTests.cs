using System;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class BrowserIdentificationTests
  {
    [Test]
    public void ToString_returns_appropriately_formatted_string()
    {
      // Arrange
      var id = new BrowserIdentification("FooBrowser", new SimpleStringVersion("123"), "BarPlatform");

      // Act
      var result = id.ToString();

      // Assert
      Assert.That(result, Is.EqualTo("FooBrowser 123 (BarPlatform)"));
    }

    [Test]
    public void ToString_returns_correct_string_for_unidentified_browser()
    {
      // Arrange
      var id = BrowserIdentification.UnidentifiedBrowser;

      // Act
      var result = id.ToString();

      // Assert
      Assert.That(result, Is.EqualTo("Unidentified browser"));
    }
  }
}

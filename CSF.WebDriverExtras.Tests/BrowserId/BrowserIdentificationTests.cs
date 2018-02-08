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
  }
}

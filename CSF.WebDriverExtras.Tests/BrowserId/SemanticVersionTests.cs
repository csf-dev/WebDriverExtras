using System;
using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class SemanticVersionTests
  {
    [TestCase("v0.1.2")]
    [TestCase("v1.0.0")]
    [TestCase("v111.222.333")]
    [TestCase("v0.1.2-alpha.1.2")]
    [TestCase("v0.1.2-wibble1.2wobble")]
    [TestCase("v0.1.2+metadata.and.again")]
    [TestCase("v0.1.2+meta1")]
    [TestCase("v0.1.2-prerelease.1.2.3+metadata.and.again")]
    public void Parse_can_roundtrip_a_valid_string(string versionString)
    {
      // Act
      var result = SemanticVersion.Parse(versionString);

      // Assert
      Assert.That(result, Is.Not.Null);
      var roundtripped = result.ToString();
      Assert.That(roundtripped, Is.EqualTo(versionString));
    }

    [Test]
    public void ToString_adds_presumed_flag_when_version_is_presumed()
    {
      // Act
      var result = SemanticVersion.Parse("v0.1.2", true);

      // Assert
      Assert.That(result.ToString(), Is.EqualTo("[presumed] v0.1.2"));
    }

    [Test]
    public void Parse_returns_null_for_a_version_without_a_v_prefix()
    {
      // Act
      var result = SemanticVersion.Parse("0.1.2");

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void Parse_returns_null_for_a_version_with_an_alphabetic_minor_version()
    {
      // Act
      var result = SemanticVersion.Parse("v0.a.2");

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void Parse_returns_null_for_a_version_with_an_empty_prerelease_identifier()
    {
      // Act
      var result = SemanticVersion.Parse("v0.1.2-foo..baz");

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void Parse_returns_null_for_a_version_with_an_empty_piece_of_metadata()
    {
      // Act
      var result = SemanticVersion.Parse("v0.1.2+foo..baz");

      // Assert
      Assert.That(result, Is.Null);
    }

    [TestCase("v0.1.2",             "v1.2.3")]
    [TestCase("v0.1.0",             "v0.1.1")]
    [TestCase("v0.1.5",             "v0.1.10")]
    [TestCase("v0.1.2-alpha",       "v0.1.2-beta")]
    [TestCase("v0.1.2-alpha.1",     "v0.1.2-alpha.2")]
    [TestCase("v0.1.2-alpha",       "v0.1.2-alpha.1")]
    [TestCase("v0.1.2-alpha+meta",  "v0.1.2-alpha.1+abc")]
    public void CompareTo_returns_minus_one_when_first_version_is_smaller_than_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = SemanticVersion.Parse(first);
      var secondVersion = SemanticVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(-1));
    }

    [TestCase("v5.1.2",             "v1.2.3")]
    [TestCase("v0.1.2",             "v0.1.1")]
    [TestCase("v0.1.50",            "v0.1.9")]
    [TestCase("v0.1.2-gamma",       "v0.1.2-beta")]
    [TestCase("v0.1.2-alpha.10",    "v0.1.2-alpha.2")]
    [TestCase("v0.1.2-alpha.1.2",   "v0.1.2-alpha.1")]
    [TestCase("v0.1.2-a.1+meta",    "v0.1.2-a+abc")]
    public void CompareTo_returns_one_when_first_version_is_greater_than_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = SemanticVersion.Parse(first);
      var secondVersion = SemanticVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(1));
    }

    [TestCase("v1.2.3",             "v1.2.3")]
    [TestCase("v0.1.1",             "v0.1.1")]
    [TestCase("v0.1.9",             "v0.1.9")]
    [TestCase("v0.1.2-gamma",       "v0.1.2-gamma")]
    [TestCase("v0.1.2-alpha.10",    "v0.1.2-alpha.10")]
    [TestCase("v0.1.2-alpha.1.2",   "v0.1.2-alpha.1.2")]
    [TestCase("v0.1.2-a.1+meta",    "v0.1.2-a.1+abc")]
    [TestCase("v0.1.2-a.1+meta",    "v0.1.2-a.1")]
    public void CompareTo_returns_zero_when_first_version_is_same_as_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = SemanticVersion.Parse(first);
      var secondVersion = SemanticVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(0));
    }
  }
}

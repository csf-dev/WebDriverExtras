using CSF.WebDriverExtras.BrowserId;
using NUnit.Framework;
using System;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class DottedNumericVersionTests
  {
    [TestCase("0.1.2")]
    [TestCase("1.0.0")]
    [TestCase("111.222.333")]
    [TestCase("0.1")]
    [TestCase("5")]
    [TestCase("0.1.2.3.4.5")]
    public void Parse_can_roundtrip_a_valid_string(string versionString)
    {
      // Act
      var result = DottedNumericVersion.Parse(versionString);

      // Assert
      Assert.That(result, Is.Not.Null);
      var roundtripped = result.ToString();
      Assert.That(roundtripped, Is.EqualTo(versionString));
    }

    [Test]
    public void ToString_adds_presumed_flag_when_version_is_presumed()
    {
      // Act
      var result = DottedNumericVersion.Parse("0.1.2", true);

      // Assert
      Assert.That(result.ToString(), Is.EqualTo("[presumed] 0.1.2"));
    }

    [Test]
    public void Parse_returns_null_for_a_version_with_an_alphabetic_component()
    {
      // Act
      var result = DottedNumericVersion.Parse("0.a.2");

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void Parse_returns_null_for_empty_string()
    {
      // Act
      var result = DottedNumericVersion.Parse(String.Empty);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void Parse_returns_null_for_a_version_which_includes_a_space()
    {
      // Act
      var result = DottedNumericVersion.Parse("0.1 1.2");

      // Assert
      Assert.That(result, Is.Null);
    }

    [TestCase("5",                  "10")]
    [TestCase("0.1.2",              "1.2.3")]
    [TestCase("0.1.0",              "0.1.1")]
    [TestCase("0.1.5",              "0.1.10")]
    [TestCase("0.1",                "0.1.2")]
    [TestCase("0.1.2.4",            "0.1.2.4.0")]
    public void CompareTo_returns_minus_one_when_first_version_is_smaller_than_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = DottedNumericVersion.Parse(first);
      var secondVersion = DottedNumericVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(-1));
    }

    [TestCase("10",                 "5")]
    [TestCase("5.1.2",              "1.2.3")]
    [TestCase("0.1.1",              "0.1.0")]
    [TestCase("0.1.10",             "0.1.5")]
    [TestCase("0.1.2",              "0.1")]
    [TestCase("0.1.2.4.15",         "0.1.2.4")]
    public void CompareTo_returns_one_when_first_version_is_greater_than_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = DottedNumericVersion.Parse(first);
      var secondVersion = DottedNumericVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(1));
    }

    [TestCase("10",                 "10")]
    [TestCase("5.1.2",              "5.1.2")]
    [TestCase("0.1.1",              "0.1.1")]
    [TestCase("0.1.10",             "0.1.10")]
    [TestCase("0.1.2",              "0.1.2")]
    [TestCase("0.1.2.4.15",         "0.1.2.4.15")]
    public void CompareTo_returns_zero_when_first_version_is_same_as_second_version(string first, string second)
    {
      // Arrange
      var firstVersion = DottedNumericVersion.Parse(first);
      var secondVersion = DottedNumericVersion.Parse(second);

      // Act
      var result = firstVersion.CompareTo(secondVersion);

      // Assert
      Assert.That(result, Is.EqualTo(0));
    }
  }
}

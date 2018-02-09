using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using Moq;
using CSF.WebDriverExtras.BrowserId;
using CSF.WebDriverExtras.Tests.BrowserId;

namespace CSF.WebDriverExtras.Tests.Flags
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class DefinitionReaderTests
  {
    const string SampleDefinitionsResourceName = "SampleFlagsDefinitions.json";

    [Test,AutoMoqData]
    public void GetFlagsDefinitions_can_get_definitions_from_stream(ICreatesBrowserVersions versionFactory)
    {
      // Arrange
      var sut = new DefinitionReader(versionFactory);
      Mock.Get(versionFactory)
          .Setup(x => x.CreateVersion(It.IsAny<string>(), It.IsAny<string>()))
          .Returns((string ver, string browser) => new SimpleStringVersion(ver));
      var expected = GetSampleDefinitionsContents();

      // Act
      IReadOnlyCollection<FlagsDefinition> result;
      using(var stream = GetSampleDefinitionsStream())
        result = sut.GetFlagsDefinitions(stream);

      // Assert
      Assert.That(result, Is.EqualTo(expected));
    }

    Stream GetSampleDefinitionsStream()
      => Assembly.GetExecutingAssembly().GetManifestResourceStream(SampleDefinitionsResourceName);

    IReadOnlyList<FlagsDefinition> GetSampleDefinitionsContents()
    {
      return new [] {
        new FlagsDefinition() {
          BrowserNames = new HashSet<string>(new [] { "FooBrowser" }),
          MinimumVersion = new SimpleStringVersion("1.2.3"),
          MaximumVersion = new SimpleStringVersion("2.0.0"),
          Platforms = new HashSet<string>(new [] { "BarPlatform" }),
          AddFlags = new HashSet<string>(new [] { "FlagOne", "FlagTwo", "FlagThree" }),
          RemoveFlags = new HashSet<string>(new [] { "BadFlagOne", "BadFlagTwo" }),
        },
        new FlagsDefinition() {
          BrowserNames = new HashSet<string>(new [] { "FooBrowser", "BarBrowser" }),
          MinimumVersion = new SimpleStringVersion("2.0.0"),
          MaximumVersion = new SimpleStringVersion("4.0.0"),
          AddFlags = new HashSet<string>(new [] { "FlagOne" }),
        },
        new FlagsDefinition() {
          BrowserNames = new HashSet<string>(new [] { "FooBrowser" }),
          MinimumVersion = new SimpleStringVersion("4.0.0"),
          RemoveFlags = new HashSet<string>(new [] { "BadFlagThree" }),
        },
      };
    }
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.FactoryBuilders;
using CSF.WebDriverExtras.Factories;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests.FactoryBuilders
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ProviderOptionsFactoryTests
  {
    [Test,AutoMoqData]
    public void GetProviderOptions_returns_null_for_unsupported_factory(ICreatesWebDriver factory,
                                                                        Dictionary<string,string> options,
                                                                        FactoryOptionsFactory sut)
    {
      // Act
      var result = sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_returns_created_instance_from_factory(ICreatesWebDriverFromOptions factory,
                                                                         FactoryOptionsFactory sut,
                                                                         SampleOptionsType expectedResult)
    {
      // Arrange
      Mock.Get(factory)
          .Setup(x => x.CreateEmptyOptions())
          .Returns(expectedResult);
      var options = new Dictionary<string,string>();

      // Act
      var result = sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result, Is.SameAs(expectedResult));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_can_set_a_string_option(ICreatesWebDriverFromOptions factory,
                                                           FactoryOptionsFactory sut,
                                                           [NoAutoProperties] SampleOptionsType expectedResult,
                                                           string expectedValue)
    {
      // Arrange
      Mock.Get(factory)
          .Setup(x => x.CreateEmptyOptions())
          .Returns(expectedResult);
      var options = new Dictionary<string,string> {
        { nameof(SampleOptionsType.AStringProperty), expectedValue },
      };

      // Act
      var result = (SampleOptionsType) sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result.AStringProperty, Is.EqualTo(expectedValue));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_can_set_a_nullable_integer(ICreatesWebDriverFromOptions factory,
                                                              FactoryOptionsFactory sut,
                                                              [NoAutoProperties] SampleOptionsType expectedResult,
                                                              int expectedValue)
    {
      // Arrange
      Mock.Get(factory)
          .Setup(x => x.CreateEmptyOptions())
          .Returns(expectedResult);
      var options = new Dictionary<string,string> {
        { nameof(SampleOptionsType.ANullableIntegerProperty), expectedValue.ToString() },
      };

      // Act
      var result = (SampleOptionsType) sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result.ANullableIntegerProperty, Is.EqualTo(expectedValue));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_can_leave_a_nullable_integer_unset(ICreatesWebDriverFromOptions factory,
                                                                      FactoryOptionsFactory sut,
                                                                      [NoAutoProperties] SampleOptionsType expectedResult)
    {
      // Arrange
      Mock.Get(factory)
          .Setup(x => x.CreateEmptyOptions())
          .Returns(expectedResult);
      var options = new Dictionary<string,string>();

      // Act
      var result = (SampleOptionsType) sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result.ANullableIntegerProperty, Is.Null);
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_can_set_a_boolean(ICreatesWebDriverFromOptions factory,
                                                              FactoryOptionsFactory sut,
                                                              [NoAutoProperties] SampleOptionsType expectedResult,
                                                              bool expectedValue)
    {
      // Arrange
      Mock.Get(factory)
          .Setup(x => x.CreateEmptyOptions())
          .Returns(expectedResult);
      var options = new Dictionary<string,string> {
        { nameof(SampleOptionsType.ABooleanProperty), expectedValue.ToString() },
      };

      // Act
      var result = (SampleOptionsType) sut.GetFactoryOptions(factory, options);

      // Assert
      Assert.That(result.ABooleanProperty, Is.EqualTo(expectedValue));
    }

    public class SampleOptionsType
    {
      public string AStringProperty { get; set; }

      public bool ABooleanProperty { get; set; }

      public int? ANullableIntegerProperty { get; set; }
    }
  }
}

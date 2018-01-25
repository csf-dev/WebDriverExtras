using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.FactoryBuilders;
using CSF.WebDriverExtras.Flags;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests.FactoryBuilders
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class WebDriverProviderFactoryCreatorTests
  {
    [Test,AutoMoqData]
    public void CreateFactory_calls_instance_creator_using_correct_type([Frozen] IInstanceCreator creator,
                                                                        WebDriverProviderFactoryCreator sut)
    {
      // Arrange
      var typeName = typeof(FactoryType).AssemblyQualifiedName;

      // Act
      sut.GetFactory(typeName);

      // Assert
      Mock.Get(creator).Verify(x => x.CreateInstance(typeof(FactoryType)), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateFactory_returns_null_for_nonexistent_type([Frozen] IInstanceCreator creator,
                                                                WebDriverProviderFactoryCreator sut,
                                                                string invalidType)
    {
      // Arrange
      Mock.Get(creator).Setup(x => x.CreateInstance(It.IsAny<Type>())).Returns((object) null);

      // Act
      var result = sut.GetFactory(invalidType);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void CreateFactory_returns_null_for_null_type([Frozen] IInstanceCreator creator,
                                                         WebDriverProviderFactoryCreator sut)
    {
      // Arrange
      Mock.Get(creator).Setup(x => x.CreateInstance(It.IsAny<Type>())).Returns((object) null);

      // Act
      var result = sut.GetFactory(null);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void CreateFactory_returns_result_from_instance_creator([Frozen] IInstanceCreator creator,
                                                                   WebDriverProviderFactoryCreator sut,
                                                                   ICreatesWebDriverProviders expectedResult)
    {
      // Arrange
      var typeName = typeof(FactoryType).AssemblyQualifiedName;
      Mock.Get(creator).Setup(x => x.CreateInstance(typeof(FactoryType))).Returns(expectedResult);

      // Act
      var result = sut.GetFactory(typeName);

      // Assert
      Assert.That(result, Is.SameAs(expectedResult));
    }

    public class FactoryType : ICreatesWebDriverProviders
    {
      public IProvidesWebDriver CreateProvider(IDictionary<string, object> requestedCapabilities = null,
                                               IGetsBrowserFlags flagsProvider = null)
      {
        throw new NotImplementedException();
      }
    }
  }
}

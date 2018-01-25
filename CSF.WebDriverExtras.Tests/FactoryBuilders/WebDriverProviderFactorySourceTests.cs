using System;
using System.Collections.Generic;
using CSF.Configuration;
using CSF.WebDriverExtras.FactoryBuilders;
using CSF.WebDriverExtras.Factories;
using Moq;
using NUnit.Framework;
using CSF.WebDriverExtras.Config;

namespace CSF.WebDriverExtras.Tests.FactoryBuilders
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class WebDriverProviderFactorySourceTests
  {
    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_null_when_config_does_not_exist(IGetsFactoryConfiguration config,
                                                                                    ICreatesWebDriverProviderFactories factoryCreator,
                                                                                    ICreatesProviderOptions optionsCreator)
    {
      // Arrange
      var sut = new WebDriverProviderFactorySource(config, factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.HasConfiguration)
          .Returns(false);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_passes_assembly_name_to_factory_creator(ICreatesWebDriverProviderFactories factoryCreator,
                                                                         ICreatesProviderOptions optionsCreator,
                                                                                    IGetsFactoryConfiguration config,
                                                                         string typeName,
                                                                                    Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverProviderFactorySource(config, factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.HasConfiguration)
          .Returns(true);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetProviderOptions())
          .Returns(optionsDictionary);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Mock.Get(factoryCreator).Verify(x => x.GetFactory(typeName), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_factory_creator_result_when_factory_does_not_support_options(ICreatesWebDriverProviderFactories factoryCreator,
                                                                                                                 ICreatesProviderOptions optionsCreator,
                                                                                                                 IGetsFactoryConfiguration config,
                                                                                                                 ICreatesWebDriverProviders factory,
                                                                                                                 string typeName,
                                                                                                                 Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverProviderFactorySource(config, factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.HasConfiguration)
          .Returns(true);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetProviderOptions())
          .Returns(optionsDictionary);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.SameAs(factory));
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_proxy_when_factory_supports_options(ICreatesWebDriverProviderFactories factoryCreator,
                                                                                        ICreatesProviderOptions optionsCreator,
                                                                                        IGetsFactoryConfiguration config,
                                                                                        ICreatesWebDriverProvidersWithOptions factory,
                                                                                        object options,
                                                                                        string typeName,
                                                                                        Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverProviderFactorySource(config, factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.HasConfiguration)
          .Returns(true);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetProviderOptions())
          .Returns(optionsDictionary);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);
      Mock.Get(optionsCreator)
          .Setup(x => x.GetProviderOptions(factory, It.IsAny<IDictionary<string,string>>()))
          .Returns(options);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.InstanceOf<OptionsCachingProviderFactoryProxy>());
      var proxy = (OptionsCachingProviderFactoryProxy) result;
      Assert.That(proxy.ProxiedProvider, Is.SameAs(factory));
    }
  }
}

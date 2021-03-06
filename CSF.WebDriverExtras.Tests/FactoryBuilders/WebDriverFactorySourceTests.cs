﻿using System;
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
  public class WebDriverFactorySourceTests
  {
    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_null_when_config_does_not_exist(ICreatesWebDriverFactory factoryCreator,
                                                                                    ICreatesFactoryOptions optionsCreator)
    {
      // Arrange
      var sut = new WebDriverFactorySource(factoryCreator, optionsCreator);

      // Act
      var result = sut.CreateFactory(null);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_passes_assembly_name_to_factory_creator(ICreatesWebDriverFactory factoryCreator,
                                                                         ICreatesFactoryOptions optionsCreator,
                                                                                    IDescribesWebDriverFactory config,
                                                                         string typeName,
                                                                                    Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverFactorySource(factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetOptionKeyValuePairs())
          .Returns(optionsDictionary);

      // Act
      var result = sut.CreateFactory(config);

      // Assert
      Mock.Get(factoryCreator).Verify(x => x.GetFactory(typeName), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_factory_creator_result_when_factory_does_not_support_options(ICreatesWebDriverFactory factoryCreator,
                                                                                                                 ICreatesFactoryOptions optionsCreator,
                                                                                                                 IDescribesWebDriverFactory config,
                                                                                                                 ICreatesWebDriver factory,
                                                                                                                 string typeName,
                                                                                                                 Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverFactorySource(factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetOptionKeyValuePairs())
          .Returns(optionsDictionary);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);

      // Act
      var result = sut.CreateFactory(config);

      // Assert
      Assert.That(result, Is.SameAs(factory));
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_proxy_when_factory_supports_options(ICreatesWebDriverFactory factoryCreator,
                                                                                        ICreatesFactoryOptions optionsCreator,
                                                                                        IDescribesWebDriverFactory config,
                                                                                        ICreatesWebDriverFromOptions factory,
                                                                                        object options,
                                                                                        string typeName,
                                                                                        Dictionary<string,string> optionsDictionary)
    {
      // Arrange
      var sut = new WebDriverFactorySource(factoryCreator, optionsCreator);
      Mock.Get(config)
          .Setup(x => x.GetFactoryAssemblyQualifiedTypeName())
          .Returns(typeName);
      Mock.Get(config)
          .Setup(x => x.GetOptionKeyValuePairs())
          .Returns(optionsDictionary);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);
      Mock.Get(optionsCreator)
          .Setup(x => x.GetFactoryOptions(factory, It.IsAny<IDictionary<string,string>>()))
          .Returns(options);

      // Act
      var result = sut.CreateFactory(config);

      // Assert
      Assert.That(result, Is.InstanceOf<OptionsCachingDriverFactoryProxy>());
      var proxy = (OptionsCachingDriverFactoryProxy) result;
      Assert.That(proxy.ProxiedFactory, Is.SameAs(factory));
    }
  }
}

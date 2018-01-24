using System;
using System.Collections.Generic;
using CSF.Configuration;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;
using Moq;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests.Config
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ConfigurationWebDriverProviderFactorySourceTests
  {
    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_null_when_config_does_not_exist(IConfigurationReader configReader,
                                                                                    ICreatesWebDriverProviderFactories factoryCreator,
                                                                                    ICreatesProviderOptions optionsCreator)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns((WebDriverProviderFactoryConfigurationSection) null);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test,AutoMoqData]
    public void HasConfiguration_returns_false_when_config_does_not_exist(IConfigurationReader configReader,
                                                                          ICreatesWebDriverProviderFactories factoryCreator,
                                                                          ICreatesProviderOptions optionsCreator)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns((WebDriverProviderFactoryConfigurationSection) null);

      // Act
      var result = sut.HasConfiguration();

      // Assert
      Assert.That(result, Is.False);
    }

    [Test,AutoMoqData]
    public void HasConfiguration_returns_true_when_config_exists(IConfigurationReader configReader,
                                                                 ICreatesWebDriverProviderFactories factoryCreator,
                                                                 ICreatesProviderOptions optionsCreator,
                                                                 WebDriverProviderFactoryConfigurationSection config)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns(config);

      // Act
      var result = sut.HasConfiguration();

      // Assert
      Assert.That(result, Is.True);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_passes_assembly_name_to_factory_creator(IConfigurationReader configReader,
                                                                         ICreatesWebDriverProviderFactories factoryCreator,
                                                                         ICreatesProviderOptions optionsCreator,
                                                                         WebDriverProviderFactoryConfigurationSection config,
                                                                         string typeName)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns(config);
      config.WebDriverFactoryAssemblyQualifiedType = typeName;

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Mock.Get(factoryCreator).Verify(x => x.GetFactory(typeName), Times.Once);
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_factory_creator_result_when_factory_does_not_support_options(IConfigurationReader configReader,
                                                                                                                 ICreatesWebDriverProviderFactories factoryCreator,
                                                                                                                 ICreatesProviderOptions optionsCreator,
                                                                                                                 WebDriverProviderFactoryConfigurationSection config,
                                                                                                                 ICreatesWebDriverProviders factory)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.SameAs(factory));
    }

    [Test,AutoMoqData]
    public void GetWebDriverProviderFactory_returns_proxy_when_factory_supports_options(IConfigurationReader configReader,
                                                                                        ICreatesWebDriverProviderFactories factoryCreator,
                                                                                        ICreatesProviderOptions optionsCreator,
                                                                                        WebDriverProviderFactoryConfigurationSection config,
                                                                                        ICreatesWebDriverProvidersWithOptions factory,
                                                                                        object options)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactorySource(configReader, factoryCreator, optionsCreator);
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverProviderFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(factoryCreator)
          .Setup(x => x.GetFactory(It.IsAny<string>()))
          .Returns(factory);
      Mock.Get(optionsCreator)
          .Setup(x => x.GetProviderOptions(factory, It.IsAny<IDictionary<string,string>>()))
          .Returns(options);

      // Act
      var result = sut.GetWebDriverProviderFactory();

      // Assert
      Assert.That(result, Is.InstanceOf<ConfigurationWebDriverProviderFactoryProxy>());
      var proxy = (ConfigurationWebDriverProviderFactoryProxy) result;
      Assert.That(proxy.ProxiedProvider, Is.SameAs(factory));
    }
  }
}

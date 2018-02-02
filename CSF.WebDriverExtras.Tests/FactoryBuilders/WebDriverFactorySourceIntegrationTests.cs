using System;
using System.Collections;
using System.Collections.Generic;
using CSF.Configuration;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.FactoryBuilders;
using CSF.WebDriverExtras.Flags;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests.FactoryBuilders
{
  [TestFixture, Parallelizable(ParallelScope.All)]
  [Description("These integration tests test the stack top-to-bottom for getting a web driver factory, except actually reading config/the environment")]
  public class WebDriverFactorySourceIntegrationTests
  {
    [Test,AutoMoqData]
    public void CreateFactoryFromConfig_returns_instance_of_options_caching_proxy(IConfigurationReader configReader,
                                                                                  IReadsEnvironmentVariables env,
                                                                                  [NoAutoProperties] WebDriverFactoryConfigurationSection config)
    {
      // Arrange
      var envVars = new Dictionary<string,string>();
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(env)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(envVars);
      config.WebDriverFactoryAssemblyQualifiedType = typeof(DummyBrowserFactory).AssemblyQualifiedName;
      config.EnvironmentVariableSupportEnabled = true;
      config.EnvironmentVariablePrefix = "Env_";

      var sut = new WebDriverFactorySource(configurationReader: configReader,
                                           environmentReader: env);

      // Act
      var result = sut.CreateFactoryFromConfig();

      // Assert
      Assert.That(result, Is.InstanceOf<OptionsCachingDriverFactoryProxy>());
    }

    [Test,AutoMoqData]
    public void CreateFactoryFromConfig_can_create_an_instance_of_a_dummy_factory(IConfigurationReader configReader,
                                                                                  IReadsEnvironmentVariables env,
                                                                                  [NoAutoProperties] WebDriverFactoryConfigurationSection config)
    {
      // Arrange
      var envVars = new Dictionary<string,string>();
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(env)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(envVars);
      config.WebDriverFactoryAssemblyQualifiedType = typeof(DummyBrowserFactory).AssemblyQualifiedName;
      config.EnvironmentVariableSupportEnabled = true;
      config.EnvironmentVariablePrefix = "Env_";

      var sut = new WebDriverFactorySource(configurationReader: configReader,
                                           environmentReader: env);

      // Act
      var factory = sut.CreateFactoryFromConfig();
      var result = GetProxiedFactory(factory);

      // Assert
      Assert.That(result, Is.InstanceOf<DummyBrowserFactory>());
    }

    [Test,AutoMoqData]
    public void CreateFactoryFromConfig_can_derive_and_cache_options_correctly(IConfigurationReader configReader,
                                                                               IReadsEnvironmentVariables env,
                                                                               [NoAutoProperties] WebDriverFactoryConfigurationSection config,
                                                                               IDictionary<string, object> caps,
                                                                               IGetsBrowserFlags flagsProvider,
                                                                               string scenarioName)
    {
      // Arrange
      var envVars = new Dictionary<string,string>();
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(env)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(envVars);

      envVars.Add("Env_AnotherDummyNumber", "20");
      envVars.Add("Env_DummyString", "Ohai");
      envVars.Add("Invalid_DummyNumber", "8");

      config.WebDriverFactoryAssemblyQualifiedType = typeof(DummyBrowserFactory).AssemblyQualifiedName;
      config.EnvironmentVariableSupportEnabled = true;
      config.EnvironmentVariablePrefix = "Env_";
      config.FactoryOptions.Add(new FactoryOption { Name = "DummyString", Value = "Should be overridden" });
      config.FactoryOptions.Add(new FactoryOption { Name = "DummyNumber", Value = "5" });

      var sut = new WebDriverFactorySource(configurationReader: configReader,
                                           environmentReader: env);

      // Act
      var factory = sut.CreateFactoryFromConfig();
      var result = GetProxiedFactory(factory);
      factory.CreateWebDriver(caps, flagsProvider, scenarioName);

      // Assert
      Assert.That(result.CapturedOptions.DummyString, Is.EqualTo("Ohai"));
      Assert.That(result.CapturedOptions.DummyNumber, Is.EqualTo(5));
      Assert.That(result.CapturedOptions.AnotherDummyNumber, Is.EqualTo(20));
    }

    [Test,AutoMoqData]
    public void CreateFactoryFromConfig_can_pass_non_option_parameters_through(IConfigurationReader configReader,
                                                                               IReadsEnvironmentVariables env,
                                                                               [NoAutoProperties] WebDriverFactoryConfigurationSection config,
                                                                               IDictionary<string, object> caps,
                                                                               IGetsBrowserFlags flagsProvider,
                                                                               string scenarioName)
    {
      // Arrange
      var envVars = new Dictionary<string,string>();
      Mock.Get(configReader)
          .Setup(x => x.ReadSection<WebDriverFactoryConfigurationSection>())
          .Returns(config);
      Mock.Get(env)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(envVars);

      config.WebDriverFactoryAssemblyQualifiedType = typeof(DummyBrowserFactory).AssemblyQualifiedName;

      var sut = new WebDriverFactorySource(configurationReader: configReader,
                                           environmentReader: env);

      // Act
      var factory = sut.CreateFactoryFromConfig();
      var result = GetProxiedFactory(factory);
      factory.CreateWebDriver(caps, flagsProvider, scenarioName);

      // Assert
      Assert.That(result.CapturedName, Is.EqualTo(scenarioName));
      Assert.That(result.CapturedCapabilities, Is.SameAs(caps));
      Assert.That(result.CapturedFlagsProvider, Is.SameAs(flagsProvider));
    }

    DummyBrowserFactory GetProxiedFactory(ICreatesWebDriver factory)
    {
      var fac = factory as OptionsCachingDriverFactoryProxy;

      if(ReferenceEquals(fac, null)) return null;

      return fac.ProxiedFactory as DummyBrowserFactory;
    }

    #region factory type

    public class DummyBrowserFactory : RemoteDriverFactoryBase<DummyBrowserOptions>
    {
      public DummyBrowserOptions CapturedOptions { get; set; }
      public IDictionary<string, object> CapturedCapabilities { get; set; }
      public IGetsBrowserFlags CapturedFlagsProvider { get; set; }
      public string CapturedName { get; set; }

      public override IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities,
                                                 DummyBrowserOptions options,
                                                 IGetsBrowserFlags flagsProvider,
                                                 string scenarioName)
      {
        CapturedCapabilities = requestedCapabilities;
        CapturedOptions = options;
        CapturedFlagsProvider = flagsProvider;
        CapturedName = scenarioName;

        return Mock.Of<IWebDriver>();
      }
    }

    #endregion

    #region options type

    public class DummyBrowserOptions
    {
      public string DummyString { get; set; }

      public int DummyNumber { get; set; }

      public int? AnotherDummyNumber { get; set; }
    }

    #endregion
  }
}

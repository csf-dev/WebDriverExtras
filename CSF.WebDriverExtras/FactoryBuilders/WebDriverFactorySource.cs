using System;
using CSF.Configuration;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A service which 'spawns' web driver factories from descriptions.
  /// </summary>
  public class WebDriverFactorySource
  {
    readonly IConfigurationReader configurationReader;
    readonly ICreatesWebDriverFactory factoryCreator;
    readonly ICreatesFactoryOptions optionsCreator;
    readonly IReadsEnvironmentVariables environmentReader;

    /// <summary>
    /// Gets a web driver factory using the application configuration.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    public virtual ICreatesWebDriver CreateFactoryFromConfig()
    {
      var description = GetDescriptionFromConfig();
      description = GetDescriptionWithEnvironmentSupportIfApplicable(description);
      return CreateFactory(description);
    }

    /// <summary>
    /// Gets a web driver factory matching the given description.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    /// <param name="description">An object which describes a web driver factory.</param>
    public virtual ICreatesWebDriver CreateFactory(IDescribesWebDriverFactory description)
    {
      if(description == null) return null;

      var factory = factoryCreator.GetFactory(description.GetFactoryAssemblyQualifiedTypeName());
      if(factory == null) return null;

      var options = optionsCreator.GetFactoryOptions(factory, description.GetOptionKeyValuePairs());

      return GetFactory(factory, options);
    }

    IDescribesWebDriverFactory GetDescriptionWithEnvironmentSupportIfApplicable(IDescribesWebDriverFactory description)
    {
      if(description == null) return null;

      if(description is IIndicatesEnvironmentSupport)
      {
        var envSupport = (IIndicatesEnvironmentSupport) description;
        if(envSupport.EnvironmentVariableSupportEnabled)
          return new EnvironmentVariableFactoryDescriptionProxy(description, environmentReader);
      }

      return description;
    }

    ICreatesWebDriver GetFactory(ICreatesWebDriver factory, object options)
    {
      if(options != null && (factory is ICreatesWebDriverFromOptions))
        return new OptionsCachingDriverFactoryProxy((ICreatesWebDriverFromOptions) factory, options);

      return factory;
    }

    IDescribesWebDriverFactory GetDescriptionFromConfig()
      => configurationReader.ReadSection<WebDriverFactoryConfigurationSection>();

    ICreatesWebDriverFactory GetFactoryCreator(ICreatesWebDriverFactory creator)
    {
      if(creator != null) return creator;

      var instanceCreator = new ActivatorInstanceCreator();
      return new WebDriverFactoryCreator(instanceCreator);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebDriverFactorySource"/> class.
    /// </summary>
    /// <param name="factoryCreator">A factory service which creates web driver factories (a factory-factory if you will).</param>
    /// <param name="optionsCreator">A factory service which instantiates instances of factory options.</param>
    /// <param name="configurationReader">A service which reads the application configuration.</param>
    /// <param name="environmentReader">A service which reads the process' environment variables.</param>
    public WebDriverFactorySource(ICreatesWebDriverFactory factoryCreator = null,
                                  ICreatesFactoryOptions optionsCreator = null,
                                  IConfigurationReader configurationReader = null,
                                  IReadsEnvironmentVariables environmentReader = null)
    {
      this.factoryCreator = GetFactoryCreator(factoryCreator);
      this.optionsCreator = optionsCreator ?? new FactoryOptionsFactory();
      this.configurationReader = configurationReader ?? new ConfigurationReader();
      this.environmentReader = environmentReader ?? new EnvironmentReader();
    }
  }
}

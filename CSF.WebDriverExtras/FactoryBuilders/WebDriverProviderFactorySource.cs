using System;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class WebDriverProviderFactorySource
  {
    readonly IGetsFactoryConfiguration configReader;
    readonly ICreatesWebDriverProviderFactories factoryCreator;
    readonly ICreatesProviderOptions optionsCreator;

    public virtual ICreatesWebDriverProviders GetWebDriverProviderFactory()
    {
      if(!configReader.HasConfiguration)
        return null;

      var factory = factoryCreator.GetFactory(configReader.GetFactoryAssemblyQualifiedTypeName());
      if(factory == null)
        return null;

      var options = optionsCreator.GetProviderOptions(factory, configReader.GetProviderOptions());

      return GetFactory(factory, options);
    }

    ICreatesWebDriverProviders GetFactory(ICreatesWebDriverProviders factory,
                                          object options)
    {
      if(options != null && (factory is ICreatesWebDriverProvidersWithOptions))
        return new OptionsCachingProviderFactoryProxy((ICreatesWebDriverProvidersWithOptions) factory, options);

      return factory;
    }

    IGetsFactoryConfiguration GetDefaultConfigurationReader()
    {
      var configFileReader = new ConfigurationFileFactoryConfigurationReader();
      var environmentBasedReader = new EnvironmentVariableFactoryConfigReaderProxy(configFileReader);

      return environmentBasedReader;
    }

    public WebDriverProviderFactorySource() : this(null, null, null) {}

    public WebDriverProviderFactorySource(IGetsFactoryConfiguration configReader,
                                          ICreatesWebDriverProviderFactories factoryCreator,
                                          ICreatesProviderOptions optionsCreator)
    {
      if(factoryCreator == null)
      {
        var instanceCreator = new ActivatorInstanceCreator();
        this.factoryCreator = new WebDriverProviderFactoryCreator(instanceCreator);
      }
      else
      {
        this.factoryCreator = factoryCreator;
      }

      this.configReader = configReader ?? GetDefaultConfigurationReader();
      this.optionsCreator = optionsCreator ?? new ProviderOptionsFactory();
    }
  }
}

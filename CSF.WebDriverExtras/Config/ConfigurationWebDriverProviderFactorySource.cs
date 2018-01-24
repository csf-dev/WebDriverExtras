using System;
using CSF.Configuration;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.Config
{
  public class ConfigurationWebDriverProviderFactorySource
  {
    readonly IConfigurationReader configReader;
    readonly ICreatesWebDriverProviderFactories factoryCreator;
    readonly ICreatesProviderOptions optionsCreator;

    public virtual bool HasConfiguration()
      => configReader.ReadSection<WebDriverProviderFactoryConfigurationSection>() != null;

    public virtual ICreatesWebDriverProviders GetWebDriverProviderFactory()
    {
      var config = configReader.ReadSection<WebDriverProviderFactoryConfigurationSection>();
      if(config == null)
        return null;

      var factory = factoryCreator.GetFactory(config.WebDriverFactoryAssemblyQualifiedType);
      if(factory == null)
        return null;

      var options = optionsCreator.GetProviderOptions(factory, config.GetFactoryOptions());

      return GetFactory(factory, options);
    }

    ICreatesWebDriverProviders GetFactory(ICreatesWebDriverProviders factory,
                                          object options)
    {
      if(options != null && (factory is ICreatesWebDriverProvidersWithOptions))
        return new ConfigurationWebDriverProviderFactoryProxy((ICreatesWebDriverProvidersWithOptions) factory, options);

      return factory;
    }

    public ConfigurationWebDriverProviderFactorySource() : this(null, null, null) {}


    public ConfigurationWebDriverProviderFactorySource(IConfigurationReader configReader,
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

      this.configReader = configReader ?? new ConfigurationReader();
      this.optionsCreator = optionsCreator ?? new ProviderOptionsFactory();
    }
  }
}

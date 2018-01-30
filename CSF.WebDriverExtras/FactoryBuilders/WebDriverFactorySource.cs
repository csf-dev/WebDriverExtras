using System;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class WebDriverFactorySource
  {
    readonly IGetsFactoryConfiguration configReader;
    readonly ICreatesWebDriverFactory factoryCreator;
    readonly ICreatesDriverOptions optionsCreator;

    public virtual ICreatesWebDriver GetWebDriverFactory()
    {
      if(!configReader.HasConfiguration)
        return null;

      var factory = factoryCreator.GetFactory(configReader.GetFactoryAssemblyQualifiedTypeName());
      if(factory == null)
        return null;

      var options = optionsCreator.GetDriverOptions(factory, configReader.GetProviderOptions());

      return GetFactory(factory, options);
    }

    ICreatesWebDriver GetFactory(ICreatesWebDriver factory,
                                          object options)
    {
      if(options != null && (factory is ICreatesWebDriverFromOptions))
        return new OptionsCachingDriverFactoryProxy((ICreatesWebDriverFromOptions) factory, options);

      return factory;
    }

    IGetsFactoryConfiguration GetDefaultConfigurationReader()
    {
      var configFileReader = new ConfigurationFileFactoryConfigurationReader();
      var environmentBasedReader = new EnvironmentVariableFactoryConfigReaderProxy(configFileReader);

      return environmentBasedReader;
    }

    public WebDriverFactorySource() : this(null, null, null) {}

    public WebDriverFactorySource(IGetsFactoryConfiguration configReader,
                                          ICreatesWebDriverFactory factoryCreator,
                                          ICreatesDriverOptions optionsCreator)
    {
      if(factoryCreator == null)
      {
        var instanceCreator = new ActivatorInstanceCreator();
        this.factoryCreator = new WebDriverFactoryCreator(instanceCreator);
      }
      else
      {
        this.factoryCreator = factoryCreator;
      }

      this.configReader = configReader ?? GetDefaultConfigurationReader();
      this.optionsCreator = optionsCreator ?? new DriverOptionsFactory();
    }
  }
}

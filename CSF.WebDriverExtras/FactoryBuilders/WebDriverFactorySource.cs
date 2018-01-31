using System;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class WebDriverFactorySource
  {
    readonly ICreatesWebDriverFactory factoryCreator;
    readonly ICreatesDriverOptions optionsCreator;

    public virtual ICreatesWebDriver GetWebDriverFactory(IDescribesWebDriverFactory config)
    {
      if(config == null) return null;

      var factory = factoryCreator.GetFactory(config.GetFactoryAssemblyQualifiedTypeName());
      if(factory == null)
        return null;

      var options = optionsCreator.GetDriverOptions(factory, config.GetOptionKeyValuePairs());

      return GetFactory(factory, options);
    }

    ICreatesWebDriver GetFactory(ICreatesWebDriver factory, object options)
    {
      if(options != null && (factory is ICreatesWebDriverFromOptions))
        return new OptionsCachingDriverFactoryProxy((ICreatesWebDriverFromOptions) factory, options);

      return factory;
    }

    public WebDriverFactorySource() : this(null, null) {}

    public WebDriverFactorySource(ICreatesWebDriverFactory factoryCreator,
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

      this.optionsCreator = optionsCreator ?? new DriverOptionsFactory();
    }
  }
}

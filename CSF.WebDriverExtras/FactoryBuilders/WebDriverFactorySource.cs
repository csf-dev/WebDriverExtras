using System;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A service which 'spawns' web driver factories from descriptions.
  /// </summary>
  public class WebDriverFactorySource
  {
    readonly ICreatesWebDriverFactory factoryCreator;
    readonly ICreatesFactoryOptions optionsCreator;

    /// <summary>
    /// Gets a web driver factory matching the given description.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    /// <param name="description">An object which describes a web driver factory.</param>
    public virtual ICreatesWebDriver GetWebDriverFactory(IDescribesWebDriverFactory description)
    {
      if(description == null) return null;

      var factory = factoryCreator.GetFactory(description.GetFactoryAssemblyQualifiedTypeName());
      if(factory == null)
        return null;

      var options = optionsCreator.GetFactoryOptions(factory, description.GetOptionKeyValuePairs());

      return GetFactory(factory, options);
    }

    ICreatesWebDriver GetFactory(ICreatesWebDriver factory, object options)
    {
      if(options != null && (factory is ICreatesWebDriverFromOptions))
        return new OptionsCachingDriverFactoryProxy((ICreatesWebDriverFromOptions) factory, options);

      return factory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebDriverFactorySource"/> class.
    /// </summary>
    public WebDriverFactorySource() : this(null, null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="WebDriverFactorySource"/> class.
    /// </summary>
    /// <param name="factoryCreator">A factory service which creates web driver factories (a factory-factory if you will).</param>
    /// <param name="optionsCreator">A factory service which instantiates instances of factory options.</param>
    public WebDriverFactorySource(ICreatesWebDriverFactory factoryCreator,
                                  ICreatesFactoryOptions optionsCreator)
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

      this.optionsCreator = optionsCreator ?? new FactoryOptionsFactory();
    }
  }
}

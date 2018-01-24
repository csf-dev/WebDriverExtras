using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.Configuration;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.Config
{
  public class ConfigurationWebDriverProviderFactorySource
  {
    const BindingFlags PropertySearchFlags
      = BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;

    readonly IConfigurationReader configReader;
    readonly IInstanceCreator instanceCreator;

    public virtual bool HasConfiguration() => GetConfiguration() != null;

    public virtual ICreatesWebDriverProviders GetWebDriverProviderFactory()
    {
      var config = GetConfiguration();
      if(config == null)
        return null;

      var type = GetFactoryType(config);
      if(type == null)
        return null;

      var factory = GetFactory(type);
      if(factory == null)
        return null;

      return GetFactory(factory, config.GetFactoryOptions());
    }

    Type GetFactoryType(WebDriverProviderFactoryConfigurationSection config)
    {
      try
      {
        return Type.GetType(config.WebDriverFactoryAssemblyQualifiedType);
      }
      catch(Exception)
      {
        // We're dealing with user data from the config, bad config just means no factory type found
        return null;
      }
    }

    ICreatesWebDriverProviders GetFactory(Type type)
    {
      object factory;

      try
      {
        factory = instanceCreator.CreateInstance(type);
      }
      catch(Exception)
      {
        // If there was a problem creating the factory just squelch the error and return null for no factory.
        return null;
      }

      return factory as ICreatesWebDriverProviders;
    }

    ICreatesWebDriverProviders GetFactory(ICreatesWebDriverProviders baseFactory,
                                          IDictionary<string,string> configuredOptions)
    {
      var factoryWithOptionsSupport = baseFactory as ICreatesWebDriverProvidersWithOptions;
      if(factoryWithOptionsSupport == null)
        return baseFactory;

      var optionsObject = factoryWithOptionsSupport.CreateEmptyProviderOptions();
      if(optionsObject == null)
        return baseFactory;

      PopulateOptions(optionsObject, configuredOptions);
      return new ConfigurationWebDriverProviderFactoryProxy(factoryWithOptionsSupport, optionsObject);
    }

    void PopulateOptions(object options, IDictionary<string,string> optionValues)
    {
      if(options == null)
        throw new ArgumentNullException(nameof(options));
      if(optionValues == null)
        throw new ArgumentNullException(nameof(optionValues));

      var optionsType = options.GetType();

      foreach(var optionName in optionValues.Keys)
      {
        PopulateOption(optionsType, options, optionName, optionValues[optionName]);
      }
    }

    void PopulateOption(Type optionsType, object options, string optionName, string optionValue)
    {
      var property = optionsType.GetProperty(optionName, PropertySearchFlags);
      if(property == null || !property.CanWrite)
        return;

      object convertedValue;
      try
      {
        convertedValue = Convert.ChangeType(optionValue, property.PropertyType);
      }
      catch(Exception)
      {
        // If we can't convert the user-supplied value into the property then skip that property.
        return;
      }

      property.SetValue(options, convertedValue);
    }

    WebDriverProviderFactoryConfigurationSection GetConfiguration()
      => configReader.ReadSection<WebDriverProviderFactoryConfigurationSection>();

    public ConfigurationWebDriverProviderFactorySource() : this(null, null) {}

    public ConfigurationWebDriverProviderFactorySource(IConfigurationReader configReader,
                                                       IInstanceCreator instanceCreator)
    {
      this.instanceCreator = instanceCreator ?? new ActivatorInstanceCreator();
      this.configReader = configReader ?? new ConfigurationReader();
    }
  }
}

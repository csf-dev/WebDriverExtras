using System;
using System.Collections.Generic;
using System.Reflection;
using CSF.WebDriverExtras.Factories;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class DriverOptionsFactory : ICreatesDriverOptions
  {
    const BindingFlags PropertySearchFlags
      = BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;

    public object GetDriverOptions(ICreatesWebDriver factory,
                                     IDictionary<string, string> optionsDictionary)
    {
      if(factory == null)
        throw new ArgumentNullException(nameof(factory));

      var factoryWithOptionsSupport = factory as ICreatesWebDriverFromOptions;
      if(factoryWithOptionsSupport == null)
        return null;

      var optionsObject = factoryWithOptionsSupport.CreateEmptyOptions();
      PopulateOptions(optionsObject, optionsDictionary);

      return optionsObject;
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

      var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

      object convertedValue;
      try
      {
        convertedValue = Convert.ChangeType(optionValue, propertyType);
      }
      catch(Exception)
      {
        // If we can't convert the user-supplied value into the property then skip that property.
        return;
      }

      property.SetValue(options, convertedValue);
    }
  }
}

using System;
using System.Configuration;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// A single factory option, exposing a name/value pair which corresponds to a public settable property
  /// on a 'factory options; type and the value to be set into that property.
  /// </summary>
  public class FactoryOption : ConfigurationElement
  {
    const string NameConfigName = @"Name", ValueConfigName = @"Value";

    /// <summary>
    /// Gets or sets the configuration property name.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty(NameConfigName, IsRequired = true)]
    public virtual string Name
    {
      get { return (string) this[NameConfigName]; }
      set { this[NameConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty(ValueConfigName, IsRequired = true)]
    public virtual string Value
    {
      get { return (string) this[ValueConfigName]; }
      set { this[ValueConfigName] = value; }
    }
  }
}

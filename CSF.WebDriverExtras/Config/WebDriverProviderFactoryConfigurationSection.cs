using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CSF.Configuration;

namespace CSF.WebDriverExtras.Config
{
  [ConfigurationPath("WebDriverFactory")]
  public class WebDriverProviderFactoryConfigurationSection : ConfigurationSection
  {
    const string WebDriverFactoryAssemblyQualifiedTypeConfigName = @"AssemblyQualifiedTypeName";
    [ConfigurationProperty(WebDriverFactoryAssemblyQualifiedTypeConfigName, IsRequired = true)]
    public virtual string WebDriverFactoryAssemblyQualifiedType
    {
      get { return (string) this[WebDriverFactoryAssemblyQualifiedTypeConfigName]; }
      set { this[WebDriverFactoryAssemblyQualifiedTypeConfigName] = value; }
    }

    const string FactoryOptionsConfigName = @"FactoryOptions";
    [ConfigurationProperty(FactoryOptionsConfigName, IsRequired = false)]
    public virtual FactoryOptionsCollection FactoryOptions
    {
      get { return (FactoryOptionsCollection) this[FactoryOptionsConfigName]; }
      set { this[FactoryOptionsConfigName] = value; }
    }

    /// <summary>
    /// Gets a collection of name/value pairs which indicate public settable properties on the factory instance
    /// and values to set into them.
    /// </summary>
    /// <returns>The factory properties.</returns>
    public virtual IDictionary<string, string> GetFactoryOptions()
    {
      if(FactoryOptions == null)
        return new Dictionary<string,string>();

      return FactoryOptions
        .Cast<FactoryOption>()
        .ToDictionary(k => k.Name, v => v.Value);
    }
  }
}

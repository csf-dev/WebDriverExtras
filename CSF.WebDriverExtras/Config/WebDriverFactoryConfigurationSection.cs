using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CSF.Configuration;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// <c>System.Configuration.ConfigurationSection</c> implementation for selecting and configuring a
  /// <see cref="ICreatesWebDriver"/> implementation.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This type is an implementation of <see cref="IDescribesWebDriverFactory"/> and also
  /// <see cref="IIndicatesEnvironmentSupport"/>, and thus it may be used as a parameter to
  /// <see cref="FactoryBuilders.WebDriverFactorySource.GetWebDriverFactory"/>.
  /// </para>
  /// </remarks>
  [ConfigurationPath("WebDriverFactory")]
  public class WebDriverFactoryConfigurationSection : ConfigurationSection, IIndicatesEnvironmentSupport
  {
    const string
      WebDriverFactoryAssemblyQualifiedTypeConfigName   = @"AssemblyQualifiedTypeName",
      FactoryOptionsConfigName                          = @"FactoryOptions",
      EnvironmentVariableSupportEnabledConfigName       = @"EnvironmentVariableSupportEnabled",
      EnvironmentVariablePrefixConfigName               = @"EnvironmentVariablePrefix";

    /// <summary>
    /// Gets or sets the assembly-qualified type name of the web driver factory type to use.
    /// </summary>
    /// <value>The assembly-qualified type name of the web driver factory.</value>
    [ConfigurationProperty(WebDriverFactoryAssemblyQualifiedTypeConfigName, IsRequired = true)]
    public virtual string WebDriverFactoryAssemblyQualifiedType
    {
      get { return (string) this[WebDriverFactoryAssemblyQualifiedTypeConfigName]; }
      set { this[WebDriverFactoryAssemblyQualifiedTypeConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets the factory options.
    /// </summary>
    /// <value>The factory options.</value>
    [ConfigurationProperty(FactoryOptionsConfigName, IsRequired = false)]
    public virtual FactoryOptionsCollection FactoryOptions
    {
      get { return (FactoryOptionsCollection) this[FactoryOptionsConfigName]; }
      set { this[FactoryOptionsConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether environment variables should be read when creating
    /// the <see cref="FactoryOptions"/>.
    /// </summary>
    /// <value><c>true</c> if environment variable support enabled; otherwise, <c>false</c>.</value>
    [ConfigurationProperty(EnvironmentVariableSupportEnabledConfigName, IsRequired = false, DefaultValue = "False")]
    public virtual bool EnvironmentVariableSupportEnabled
    {
      get { return (bool) this[EnvironmentVariableSupportEnabledConfigName]; }
      set { this[EnvironmentVariableSupportEnabledConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets the prefix for environment variables which control the <see cref="FactoryOptions"/>.
    /// </summary>
    /// <value>The environment variable prefix.</value>
    [ConfigurationProperty(EnvironmentVariablePrefixConfigName,
                           IsRequired = false,
                           DefaultValue = EnvironmentVariableFactoryDescriptionProxy.DefaultEnvironmentVariablePrefix)]
    public virtual string EnvironmentVariablePrefix
    {
      get { return (string) this[EnvironmentVariablePrefixConfigName]; }
      set { this[EnvironmentVariablePrefixConfigName] = value; }
    }

    /// <summary>
    /// Gets a collection of key/value pairs which describe public settable properties of a 'webdriver factory options'
    /// type, along with the values for those properties.
    /// </summary>
    /// <returns>The option key/value pairs.</returns>
    #region IGetsFactoryConfiguration and IIndicatesEnvironmentSupport implementation

    public virtual IDictionary<string, string> GetOptionKeyValuePairs()
    {
      if(FactoryOptions == null)
        return new Dictionary<string,string>();

      return FactoryOptions
        .Cast<FactoryOption>()
        .ToDictionary(k => k.Name, v => v.Value);
    }

    /// <summary>
    /// Gets the prefix for environment variables which control the <see cref="FactoryOptions"/>.
    /// </summary>
    public string GetEnvironmentVariablePrefix() => EnvironmentVariableSupportEnabled? EnvironmentVariablePrefix : null;

    /// <summary>
    /// Gets the assembly-qualified type name for the web driver factory.
    /// </summary>
    /// <returns>The assembly-qualified type name.</returns>
    public string GetFactoryAssemblyQualifiedTypeName() => WebDriverFactoryAssemblyQualifiedType;

    #endregion
  }
}

using System;
using System.Collections.Generic;
using CSF.Configuration;

namespace CSF.WebDriverExtras.Config
{
  public class ConfigurationFileFactoryConfigurationReader : IGetsFactoryConfiguration
  {
    readonly IConfigurationReader configReader;

    public bool HasConfiguration => GetConfig() != null;

    public string GetFactoryAssemblyQualifiedTypeName() => GetConfig()?.WebDriverFactoryAssemblyQualifiedType;

    public IDictionary<string, string> GetProviderOptions() => GetConfig()?.GetFactoryOptions();

    WebDriverProviderFactoryConfigurationSection GetConfig()
      => configReader.ReadSection<WebDriverProviderFactoryConfigurationSection>();

    public ConfigurationFileFactoryConfigurationReader() : this(null) {}

    public ConfigurationFileFactoryConfigurationReader(IConfigurationReader configReader)
    {
      this.configReader = configReader ?? new ConfigurationReader();
    }
  }
}

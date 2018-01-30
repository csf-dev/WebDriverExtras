using System;
using System.Collections.Generic;
using CSF.Configuration;

namespace CSF.WebDriverExtras.Config
{
  public class ConfigurationFileFactoryConfigurationReader : IGetsFactoryConfiguration, IIndicatesEnvironmentSupport
  {
    readonly IConfigurationReader configReader;

    public bool HasConfiguration => GetConfig() != null;

    public bool EnvironmentVariableSupportEnabled
      => (GetConfig()?.EnvironmentVariableSupportEnabled).GetValueOrDefault();

    public string GetFactoryAssemblyQualifiedTypeName() => GetConfig()?.WebDriverFactoryAssemblyQualifiedType;

    public IDictionary<string, string> GetProviderOptions() => GetConfig()?.GetFactoryOptions();

    public WebDriverFactoryConfigurationSection GetConfig()
      => configReader.ReadSection<WebDriverFactoryConfigurationSection>();

    public string GetEnvironmentVariablePrefix() => GetConfig()?.EnvironmentVariablePrefix;

    public ConfigurationFileFactoryConfigurationReader() : this(null) {}

    public ConfigurationFileFactoryConfigurationReader(IConfigurationReader configReader)
    {
      this.configReader = configReader ?? new ConfigurationReader();
    }
  }
}

using System;
using CSF.Configuration;
using CSF.WebDriverExtras.Config;

namespace CSF.WebDriverExtras
{
  public static class GetWebDriverFactory
  {
    static readonly IConfigurationReader configurationReader;
    static readonly FactoryBuilders.WebDriverFactorySource factorySource;

    public static ICreatesWebDriver FromConfiguration(bool disableEnvironmentSupport = false)
    {
      var config = configurationReader.ReadSection<WebDriverFactoryConfigurationSection>();
      return FromConfiguration(config, disableEnvironmentSupport);
    }

    public static ICreatesWebDriver FromConfiguration(IDescribesWebDriverFactory config,
                                                      bool disableEnvironmentSupport = false)
    {
      var configToUse = config;

      if(!disableEnvironmentSupport && (config is IIndicatesEnvironmentSupport))
      {
        var envSupport = (IIndicatesEnvironmentSupport) config;
        if(envSupport.EnvironmentVariableSupportEnabled)
          configToUse = new EnvironmentVariableFactoryConfigProxy(config);
      }

      return factorySource.GetWebDriverFactory(configToUse);
    }

    static GetWebDriverFactory()
    {
      factorySource = new FactoryBuilders.WebDriverFactorySource();
      configurationReader = new ConfigurationReader();
    }
  }
}

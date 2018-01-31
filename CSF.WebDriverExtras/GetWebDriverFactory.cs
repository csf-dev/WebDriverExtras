using System;
using CSF.Configuration;
using CSF.WebDriverExtras.Config;

namespace CSF.WebDriverExtras
{
  /// <summary>
  /// Static helper type which provides a shortcut to getting web driver factories.
  /// </summary>
  public static class GetWebDriverFactory
  {
    static readonly IConfigurationReader configurationReader;
    static readonly FactoryBuilders.WebDriverFactorySource factorySource;

    /// <summary>
    /// Reads the application configuration and creates/returns a web driver factory which matches the
    /// description there.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    /// <param name="disableEnvironmentSupport">If set to <c>true</c> then support for reading information
    /// from the system environment is disabled (regardless of the configuration's setting).</param>
    public static ICreatesWebDriver FromConfiguration(bool disableEnvironmentSupport = false)
    {
      var description = configurationReader.ReadSection<WebDriverFactoryConfigurationSection>();
      return FromDescription(description, disableEnvironmentSupport);
    }

    /// <summary>
    /// Reads a given web driver factory description and creates/returns a web driver factory which matches that.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    /// <param name="description">The description of a web driver factory.</param>
    /// <param name="disableEnvironmentSupport">If set to <c>true</c> then support for reading information
    /// from the system environment is disabled (regardless of the configuration's setting).</param>
    public static ICreatesWebDriver FromDescription(IDescribesWebDriverFactory description,
                                                      bool disableEnvironmentSupport = false)
    {
      var descriptionToUse = description;

      if(!disableEnvironmentSupport && (description is IIndicatesEnvironmentSupport))
      {
        var envSupport = (IIndicatesEnvironmentSupport) description;
        if(envSupport.EnvironmentVariableSupportEnabled)
          descriptionToUse = new EnvironmentVariableFactoryDescriptionProxy(description);
      }

      return factorySource.GetWebDriverFactory(descriptionToUse);
    }

    static GetWebDriverFactory()
    {
      factorySource = new FactoryBuilders.WebDriverFactorySource();
      configurationReader = new ConfigurationReader();
    }
  }
}

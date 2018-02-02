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
    static readonly FactoryBuilders.WebDriverFactorySource factorySource;

    /// <summary>
    /// Reads the application configuration and creates/returns a web driver factory which matches the
    /// description there.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    public static ICreatesWebDriver FromConfiguration() => factorySource.CreateFactoryFromConfig();

    static GetWebDriverFactory()
    {
      factorySource = new FactoryBuilders.WebDriverFactorySource();
    }
  }
}

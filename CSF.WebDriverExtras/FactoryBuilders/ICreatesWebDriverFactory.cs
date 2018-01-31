using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A factory service which creates web driver factories (a factory-factory if you will).
  /// </summary>
  public interface ICreatesWebDriverFactory
  {
    /// <summary>
    /// Gets the web driver factory based upon its assembly-qualified type name.
    /// </summary>
    /// <returns>The web driver factory.</returns>
    /// <param name="assemblyQualifiedTypeName">Assembly qualified type name.</param>
    ICreatesWebDriver GetFactory(string assemblyQualifiedTypeName);
  }
}

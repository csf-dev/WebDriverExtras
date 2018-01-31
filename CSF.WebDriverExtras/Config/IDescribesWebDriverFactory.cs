using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// Describes a type which implements <see cref="ICreatesWebDriver"/>, along with a set of options which should be
  /// used with that type.
  /// </summary>
  public interface IDescribesWebDriverFactory
  {
    /// <summary>
    /// Gets the assembly-qualified type name for the web driver factory.
    /// </summary>
    /// <returns>The assembly-qualified type name.</returns>
    string GetFactoryAssemblyQualifiedTypeName();

    /// <summary>
    /// Gets a collection of key/value pairs which describe public settable properties of a 'webdriver factory options'
    /// type, along with the values for those properties.
    /// </summary>
    /// <returns>The option key/value pairs.</returns>
    IDictionary<string,string> GetOptionKeyValuePairs();
  }
}

using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A factory service which creates and populates instances of web driver factory options.
  /// </summary>
  public interface ICreatesFactoryOptions
  {
    /// <summary>
    /// Gets the factory options.
    /// </summary>
    /// <returns>The factory options.</returns>
    /// <param name="factory">A web driver factory.</param>
    /// <param name="optionsDictionary">A collection of key-value pairs which indicate the values to populate into the created options instance.</param>
    object GetFactoryOptions(ICreatesWebDriver factory, IDictionary<string,string> optionsDictionary);
  }
}

using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// A service which gets key/value pairs of environment variables.
  /// </summary>
  public interface IReadsEnvironmentVariables
  {
    /// <summary>
    /// Gets a collection of key/value pairs of environment variables and their values.
    /// </summary>
    /// <returns>The environment variables.</returns>
    IDictionary<string,string> GetEnvironmentVariables();
  }
}

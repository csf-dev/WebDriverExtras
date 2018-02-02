using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// Implementation of <see cref="IReadsEnvironmentVariables"/> which reads real environment variables.
  /// </summary>
  public class EnvironmentReader : IReadsEnvironmentVariables
  {
    /// <summary>
    /// Gets a collection of key/value pairs of environment variables and their values.
    /// </summary>
    /// <returns>The environment variables.</returns>
    public IDictionary<string, string> GetEnvironmentVariables()
    {
      var allVariables = Environment.GetEnvironmentVariables();

      return (from variableName in allVariables.Keys.Cast<string>()
              select new { Variable = variableName, Value = (string) allVariables[variableName] })
        .ToDictionary(k => k.Variable, v => v.Value);
    }
  }
}

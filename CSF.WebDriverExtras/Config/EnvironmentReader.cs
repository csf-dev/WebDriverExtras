using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Config
{
  public class EnvironmentReader : IReadsEnvironmentVariables
  {
    public IDictionary<string, string> GetEnvironmentVariables()
    {
      var allVariables = Environment.GetEnvironmentVariables();

      return (from variableName in allVariables.Keys.Cast<string>()
              select new { Variable = variableName, Value = (string) allVariables[variableName] })
        .ToDictionary(k => k.Variable, v => v.Value);
    }
  }
}

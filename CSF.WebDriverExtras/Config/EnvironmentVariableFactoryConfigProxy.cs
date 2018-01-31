using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Config
{
  public class EnvironmentVariableFactoryConfigProxy : IDescribesWebDriverFactory
  {
    internal const string DefaultEnvironmentVariablePrefix = "WebDriver_";

    readonly IDescribesWebDriverFactory proxiedReader;
    readonly IReadsEnvironmentVariables environmentReader;

    protected IIndicatesEnvironmentSupport EnvironmentConfig => proxiedReader as IIndicatesEnvironmentSupport;

    public string GetFactoryAssemblyQualifiedTypeName() => proxiedReader.GetFactoryAssemblyQualifiedTypeName();

    public IDictionary<string, string> GetOptionKeyValuePairs()
    {
      var baseOptions = proxiedReader.GetOptionKeyValuePairs();
      if(!(EnvironmentConfig?.EnvironmentVariableSupportEnabled).GetValueOrDefault())
        return baseOptions;

      var variables = GetWebDriverEnvironmentVariables();
      return MergeEnvironmentVariablesWithOptions(baseOptions, variables);
    }

    IDictionary<string,string> GetWebDriverEnvironmentVariables()
    {
      var prefix = EnvironmentConfig.GetEnvironmentVariablePrefix() ?? DefaultEnvironmentVariablePrefix;

      return (from variable in environmentReader.GetEnvironmentVariables()
              where variable.Key.StartsWith(prefix, StringComparison.InvariantCulture)
              let nameWithoutPrefix = variable.Key.Substring(prefix.Length)
              select new { Variable = nameWithoutPrefix, Value = variable.Value })
        .ToDictionary(k => k.Variable, v => v.Value);
    }

    IDictionary<string,string> MergeEnvironmentVariablesWithOptions(IDictionary<string,string> options,
                                                                    IDictionary<string,string> environmentVariables)
    {
      var output = new Dictionary<string,string>(options);

      foreach(var variable in environmentVariables)
        output[variable.Key] = variable.Value;
      
      return output;
    }

    public EnvironmentVariableFactoryConfigProxy(IDescribesWebDriverFactory proxiedReader)
      : this(proxiedReader, null) {}
    
    public EnvironmentVariableFactoryConfigProxy(IDescribesWebDriverFactory proxiedReader,
                                                 IReadsEnvironmentVariables environmentReader)
    {
      if(proxiedReader == null)
        throw new ArgumentNullException(nameof(proxiedReader));

      this.proxiedReader = proxiedReader;
      this.environmentReader = environmentReader ?? new EnvironmentReader();
    }
  }
}

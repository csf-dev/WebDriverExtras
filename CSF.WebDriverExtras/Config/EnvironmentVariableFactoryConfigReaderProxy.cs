using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Config
{
  public class EnvironmentVariableFactoryConfigReaderProxy : IGetsFactoryConfiguration
  {
    internal const string DefaultEnvironmentVariablePrefix = "WebDriver_";

    readonly IGetsFactoryConfiguration proxiedReader;
    readonly IReadsEnvironmentVariables environmentReader;

    protected IIndicatesEnvironmentSupport EnvironmentConfig => proxiedReader as IIndicatesEnvironmentSupport;

    public bool HasConfiguration => proxiedReader.HasConfiguration;

    public string GetFactoryAssemblyQualifiedTypeName() => proxiedReader.GetFactoryAssemblyQualifiedTypeName();

    public IDictionary<string, string> GetProviderOptions()
    {
      var baseOptions = proxiedReader.GetProviderOptions();
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

    public EnvironmentVariableFactoryConfigReaderProxy(IGetsFactoryConfiguration proxiedReader)
      : this(proxiedReader, null) {}
    
    public EnvironmentVariableFactoryConfigReaderProxy(IGetsFactoryConfiguration proxiedReader,
                                                       IReadsEnvironmentVariables environmentReader)
    {
      if(proxiedReader == null)
        throw new ArgumentNullException(nameof(proxiedReader));

      this.proxiedReader = proxiedReader;
      this.environmentReader = environmentReader ?? new EnvironmentReader();
    }
  }
}

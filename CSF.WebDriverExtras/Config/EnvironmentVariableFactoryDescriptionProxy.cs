using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// Proxy type for an instance of <see cref="IDescribesWebDriverFactory"/>, which is responsible for supplementing
  /// <see cref="GetOptionKeyValuePairs"/> with data from environment variables.
  /// </summary>
  public class EnvironmentVariableFactoryDescriptionProxy : IDescribesWebDriverFactory
  {
    internal const string DefaultEnvironmentVariablePrefix = "WebDriver_";

    readonly IDescribesWebDriverFactory proxiedReader;
    readonly IReadsEnvironmentVariables environmentReader;

    /// <summary>
    /// Gets <see cref="IDescribesWebDriverFactory"/> being proxied, cast to <see cref="IIndicatesEnvironmentSupport"/>.
    /// </summary>
    /// <value>The environment config.</value>
    protected IIndicatesEnvironmentSupport EnvironmentConfig => proxiedReader as IIndicatesEnvironmentSupport;

    /// <summary>
    /// Gets the assembly-qualified type name for the web driver factory.
    /// </summary>
    /// <returns>The assembly-qualified type name.</returns>
    public string GetFactoryAssemblyQualifiedTypeName() => proxiedReader.GetFactoryAssemblyQualifiedTypeName();

    /// <summary>
    /// Gets a collection of key/value pairs which describe public settable properties of a 'webdriver factory options'
    /// type, along with the values for those properties.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The implementation of this method on this type will make use of envirnment variables from an
    /// <see cref="IReadsEnvironmentVariables"/> service, in order to supplement and override the options present
    /// on the underlying proxied service.
    /// </para>
    /// </remarks>
    /// <returns>The option key/value pairs.</returns>
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

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EnvironmentVariableFactoryDescriptionProxy"/> class.
    /// </summary>
    /// <param name="proxied">The web driver factory description to proxy.</param>
    public EnvironmentVariableFactoryDescriptionProxy(IDescribesWebDriverFactory proxied)
      : this(proxied, null) {}

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EnvironmentVariableFactoryDescriptionProxy"/> class.
    /// </summary>
    /// <param name="proxied">The web driver factory description to proxy.</param>
    /// <param name="environmentReader">Environment reader.</param>
    public EnvironmentVariableFactoryDescriptionProxy(IDescribesWebDriverFactory proxied,
                                                 IReadsEnvironmentVariables environmentReader)
    {
      if(proxied == null)
        throw new ArgumentNullException(nameof(proxied));

      this.proxiedReader = proxied;
      this.environmentReader = environmentReader ?? new EnvironmentReader();
    }
  }
}

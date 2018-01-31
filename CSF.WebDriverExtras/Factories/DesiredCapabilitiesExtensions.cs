using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Extension methods for the Selenium <c>DesiredCapabilities</c> type.
  /// </summary>
  public static class DesiredCapabilitiesExtensions
  {
    /// <summary>
    /// Sets a named capability into the capabilities object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method first attempts to find the named capability in the requested capabiltiies key/value pairs.
    /// If it is not there, then a collection of fallback values are 'consulted', making use of the first such
    /// value which is not null.
    /// </para>
    /// <para>
    /// If the requested capabilities does not contain the capability and no non-null fallback can be found, then
    /// an exception is raised.
    /// </para>
    /// </remarks>
    /// <param name="capabilities">The instance into which the capability should be set.</param>
    /// <param name="name">The capability name to set.</param>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating requested capabilities and their values.</param>
    /// <param name="fallbackValues">An optional collection of fallback values for the capability.</param>
    public static void SetCapability(this DesiredCapabilities capabilities,
                                     string name,
                                     IDictionary<string,object> requestedCapabilities,
                                     params object[] fallbackValues)
    {
      if(capabilities == null)
        throw new ArgumentNullException(nameof(capabilities));
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      var val = GetCapabilityValue(name, requestedCapabilities, fallbackValues);

      if(ReferenceEquals(val, null))
        throw new ArgumentException($"Either the {nameof(requestedCapabilities)} or {nameof(fallbackValues)} must contain a non-null value for capability '{name}'");

      capabilities.SetCapability(name, val);
    }

    /// <summary>
    /// Sets a named capability into the capabilities object; skips setting the capability if no value can be found.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method first attempts to find the named capability in the requested capabiltiies key/value pairs.
    /// If it is not there, then a collection of fallback values are 'consulted', making use of the first such
    /// value which is not null.
    /// </para>
    /// <para>
    /// If the requested capabilities does not contain the capability and no non-null fallback can be found, then
    /// the setting of the capability is skipped altogether.
    /// </para>
    /// </remarks>
    /// <param name="capabilities">The instance into which the capability should be set.</param>
    /// <param name="name">The capability name to set.</param>
    /// <param name="requestedCapabilities">A collection of key/value pairs indicating requested capabilities and their values.</param>
    /// <param name="fallbackValues">An optional collection of fallback values for the capability.</param>
    public static void SetOptionalCapability(this DesiredCapabilities capabilities,
                                             string name,
                                             IDictionary<string,object> requestedCapabilities,
                                             params object[] fallbackValues)
    {
      if(capabilities == null)
        throw new ArgumentNullException(nameof(capabilities));
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      var val = GetCapabilityValue(name, requestedCapabilities, fallbackValues);

      if(!ReferenceEquals(val, null))
        capabilities.SetCapability(name, val);
    }

    static object GetCapabilityValue(string name,
                                     IDictionary<string,object> requestedCapabilities,
                                     object[] fallbackValues)
    {
      if((requestedCapabilities?.ContainsKey(name)).GetValueOrDefault()
         && requestedCapabilities[name] != null)
      {
        return requestedCapabilities[name];
      }

      return GetFallbackValue(fallbackValues);
    }

    static object GetFallbackValue(object[] values)
    {
      if(values == null)
        return null;

      return values.FirstOrDefault(x => x != null);
    }
  }
}

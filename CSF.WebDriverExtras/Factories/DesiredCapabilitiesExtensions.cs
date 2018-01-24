using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public static class DesiredCapabilitiesExtensions
  {
    public static void SetCapability(this DesiredCapabilities capabilities,
                                     string name,
                                     IDictionary<string,object> requestedCapabilities,
                                     object fallbackValue)
    {
      if(capabilities == null)
        throw new ArgumentNullException(nameof(capabilities));
      if(name == null)
        throw new ArgumentNullException(nameof(name));

      if((requestedCapabilities?.ContainsKey(name)).GetValueOrDefault()
         && requestedCapabilities[name] != null)
      {
        capabilities.SetCapability(name, requestedCapabilities[name]);
        return;
      }

      if(ReferenceEquals(fallbackValue, null))
        throw new ArgumentException($"Either the {nameof(requestedCapabilities)} or {nameof(fallbackValue)} must contain a non-null value for capability '{name}'");

      capabilities.SetCapability(name, fallbackValue);
    }

    public static void SetOptionalCapability(this DesiredCapabilities capabilities,
                                             string name,
                                             IDictionary<string,object> requestedCapabilities,
                                             object fallbackValue)
    {
      if(capabilities == null)
        throw new ArgumentNullException(nameof(capabilities));
      if(name == null)
        throw new ArgumentNullException(nameof(name));
      
      if((requestedCapabilities?.ContainsKey(name)).GetValueOrDefault()
         && requestedCapabilities[name] != null)
      {
        capabilities.SetCapability(name, requestedCapabilities[name]);
        return;
      }

      if(!ReferenceEquals(fallbackValue, null))
        capabilities.SetCapability(name, fallbackValue);
    }
  }
}

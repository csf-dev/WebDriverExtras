using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Config
{
  public interface IGetsFactoryConfiguration
  {
    bool HasConfiguration { get; }

    string GetFactoryAssemblyQualifiedTypeName();

    IDictionary<string,string> GetProviderOptions();
  }
}

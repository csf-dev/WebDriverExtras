using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public interface ICreatesProviderOptions
  {
    object GetProviderOptions(ICreatesWebDriverProviders factory, IDictionary<string,string> optionsDictionary);
  }
}

using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Config
{
  public interface ICreatesProviderOptions
  {
    object GetProviderOptions(ICreatesWebDriverProviders factory, IDictionary<string,string> optionsDictionary);
  }
}

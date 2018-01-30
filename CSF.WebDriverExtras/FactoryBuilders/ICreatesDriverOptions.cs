using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public interface ICreatesDriverOptions
  {
    object GetDriverOptions(ICreatesWebDriver factory, IDictionary<string,string> optionsDictionary);
  }
}

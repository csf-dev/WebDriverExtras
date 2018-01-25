using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras.Config
{
  public interface IReadsEnvironmentVariables
  {
    IDictionary<string,string> GetEnvironmentVariables();
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSF.WebDriverExtras.Flags
{
  public interface IReadsFlagsDefinitions
  {
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(Stream inputStream);

    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString);

    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString, Encoding encoding);
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  public class BrowserFlagsProvider : IGetsBrowserFlags
  {
    readonly IReadOnlyCollection<FlagsDefinition> flagsDefinitions;
    readonly IGetsBrowserIdentification identificationFactory;

    public IReadOnlyCollection<string> GetFlags(IHasCapabilities webDriver)
    {
      var identification = identificationFactory.GetIdentification(webDriver);

      var matchingDefinitions = flagsDefinitions.Where(x => x.Matches(identification));

      var flagsToInclude = matchingDefinitions.SelectMany(x => x.AddFlags).Distinct();
      var flagsToExclude = matchingDefinitions.SelectMany(x => x.RemoveFlags).Distinct();

      return flagsToInclude.Except(flagsToExclude).ToArray();
    }

    public BrowserFlagsProvider(IReadOnlyCollection<FlagsDefinition> flagsDefinitions) : this(flagsDefinitions, null) {}

    public BrowserFlagsProvider(IReadOnlyCollection<FlagsDefinition> flagsDefinitions,
                                IGetsBrowserIdentification identificationFactory)
    {
      if(flagsDefinitions == null)
        throw new ArgumentNullException(nameof(flagsDefinitions));
      
      this.flagsDefinitions = flagsDefinitions;
      this.identificationFactory = identificationFactory ?? new BrowserIdentificationFactory();
    }
  }
}

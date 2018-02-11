using System;
using System.Collections.Generic;
using System.Linq;
using CSF.WebDriverExtras.BrowserId;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Implementation of <see cref="IGetsBrowserFlags"/> which caches a collection of all of the available
  /// flags definitions and then returns the applicable flags matching a given web driver.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This operates by making use of the <see cref="FlagsDefinition.Matches"/> method on each of the available
  /// definitions.  Once the matching definitions are found, all of their flags to include are retrieved, along with
  /// all of their flags to remove.  Duplicates are removed from both collections.
  /// </para>
  /// <para>
  /// Finally, all of the flags to remove are removed from the flags to include, and that forms the output.
  /// </para>
  /// </remarks>
  public class BrowserFlagsProvider : IGetsBrowserFlags
  {
    readonly IReadOnlyCollection<FlagsDefinition> flagsDefinitions;

    /// <summary>
    /// Gets the browser flags which apply to the given web driver.
    /// </summary>
    /// <returns>The flags.</returns>
    /// <param name="browserId">The browser identification.</param>
    public IReadOnlyCollection<string> GetFlags(BrowserIdentification browserId)
    {
      if(browserId == null)
        throw new ArgumentNullException(nameof(browserId));

      var matchingDefinitions = flagsDefinitions.Where(x => x.Matches(browserId));

      var flagsToInclude = matchingDefinitions.SelectMany(x => x.AddFlags).Distinct();
      var flagsToExclude = matchingDefinitions.SelectMany(x => x.RemoveFlags).Distinct();

      return flagsToInclude.Except(flagsToExclude).ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserFlagsProvider"/> class.
    /// </summary>
    /// <param name="flagsDefinitions">A collection of all of the available flags definitions to consider.</param>
    public BrowserFlagsProvider(IReadOnlyCollection<FlagsDefinition> flagsDefinitions)
    {
      if(flagsDefinitions == null)
        throw new ArgumentNullException(nameof(flagsDefinitions));
      
      this.flagsDefinitions = flagsDefinitions;
    }
  }
}

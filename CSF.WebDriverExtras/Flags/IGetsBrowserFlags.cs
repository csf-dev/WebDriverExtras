using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.BrowserId;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// A service which provides a collection of the 'flags' which apply to a given web driver.
  /// </summary>
  public interface IGetsBrowserFlags
  {
    /// <summary>
    /// Gets the browser flags which apply to the given web driver.
    /// </summary>
    /// <returns>The flags.</returns>
    /// <param name="browserId">The browser identification.</param>
    IReadOnlyCollection<string> GetFlags(BrowserIdentification browserId);
  }
}

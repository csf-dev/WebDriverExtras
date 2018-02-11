using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.BrowserId;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Implementation of <see cref="IGetsBrowserFlags"/> which always returns an empty collection of flags.
  /// </summary>
  public class EmptyFlagsProvider : IGetsBrowserFlags
  {
    static IReadOnlyCollection<string> Empty => new string[0];

    /// <summary>
    /// Gets the browser flags which apply to the given web driver.
    /// </summary>
    /// <returns>The flags.</returns>
    /// <param name="browserId">Browser identification.</param>
    public IReadOnlyCollection<string> GetFlags(BrowserIdentification browserId) => Empty;
  }
}

using System;
using System.Collections.Generic;

namespace CSF.WebDriverExtras
{
  /// <summary>
  /// A web driver instance that has browser flags.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Browser flags are arbitrary string flags which are used to mark 'quirks' and behavioural features of a web
  /// driver, which existing Selenium functionality does not handle.
  /// </para>
  /// <para>
  /// For example, no version of Internet Explorer has native support for the &lt;input type="date" /&gt; element.
  /// Thus, in Internet Explorer one must treat such an element as an &lt;input type="text" /&gt; element instead.
  /// However, other browser have their own native support and thus they must be treated in a different manner.
  /// </para>
  /// <para>
  /// These flags are similar to old-stye "capability detection" in the early days of progressive enhancement.
  /// They are applied-to/stored with the web driver when the driver is created, and may then be queried later in the
  /// driver's lifetime.  Code which queries these flags does not have to be aware of "how" the web driver was created
  /// (for example, what its creation settings were).
  /// </para>
  /// </remarks>
  public interface IHasFlags
  {
    /// <summary>
    /// Gets a collection of all of the flags associated with the current driver.
    /// </summary>
    /// <returns>The flags.</returns>
    IReadOnlyCollection<string> GetFlags();

    /// <summary>
    /// Gets a value indicating whether or not the current driver has a given flag.
    /// </summary>
    /// <returns><c>true</c>, if the flag is present on the driver, <c>false</c> otherwise.</returns>
    /// <param name="flag">A flag name.</param>
    bool HasFlag(string flag);

    /// <summary>
    /// Analyses an ordered collection of web driver flags and returns the first of which that is present
    /// upon the current web driver.  Returns <c>null</c> if none of the provided flags were present.
    /// </summary>
    /// <returns>The first flag present on this driver.</returns>
    /// <param name="flags">A collection of flags.</param>
    string GetFirstFlagPresent(params string[] flags);

    /// <summary>
    /// Analyses an ordered collection of web driver flags and returns the first of which that is present
    /// upon the current web driver.  Returns <c>null</c> if none of the provided flags were present.
    /// </summary>
    /// <returns>The first flag present on this driver.</returns>
    /// <param name="flags">A collection of flags.</param>
    string GetFirstFlagPresent(IList<string> flags);
  }
}

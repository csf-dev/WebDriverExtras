using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Represents a single definition which indicates that a particular browser/version/platform combination should
  /// have (and/or not have) a set of browser flags.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Of the properties of this type, the only fully mandatory one is that the <see cref="BrowserNames"/> must contain
  /// at least one value.  This definition will only be used for the named browsers.
  /// </para>
  /// <para>
  /// The properties <see cref="MinimumVersion"/> and <see cref="MaximumVersion"/> are both optional.
  /// If the minimum version is omitted then all versions lower than the maximum version will match.
  /// If the maximum version is omitted then all versions equal-to-or-higher-than the minimum version will match.
  /// If both are omitted then all browser versions will match.
  /// </para>
  /// <para>
  /// Platforms is completely optional.  If omitted then platform will not be used to determine a match or not,
  /// however if at least one platform is provided then this definition will ONLY match if one of the platforms
  /// is equal to the browser's platform.
  /// </para>
  /// <para>
  /// Finally, for this definition to be useful, at least one value must be provided to either <see cref="AddFlags"/>
  /// and/or <see cref="RemoveFlags"/>.  These two collections indicate the flags which should be added and which
  /// should be removed from browsers.
  /// </para>
  /// <para>
  /// It is best practice to avoid using the 'remove' flags where at all possible.  Always prefer using min/max version
  /// filtering to ensure that browsers are not assigned flags which they should not have.  However, particularly when
  /// new browser versions are released and you are overriding 'stock' flags definitions with hand-crafted ones,
  /// you may need to use the 'remove' flags collection to remove flags for which support has been removed.
  /// </para>
  /// </remarks>
  public class FlagsDefinition : IEquatable<FlagsDefinition>
  {
    ISet<string> platforms, browserNames, addFlags, removeFlags;

    /// <summary>
    /// Gets or sets a collection of the browser names to which this definition applies.
    /// </summary>
    /// <value>The browser names.</value>
    public ISet<string> BrowserNames
    {
      get { return browserNames; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        browserNames = value;
      }
    }

    /// <summary>
    /// Gets or sets the minimum version (inclusive) of the browsers named by <see cref="BrowserNames"/> to which this
    /// definition applies.
    /// </summary>
    /// <value>The minimum version.</value>
    public BrowserVersion MinimumVersion { get; set; }

    /// <summary>
    /// Gets or sets the maximum version (exclusive) of the browsers named by <see cref="BrowserNames"/> to which this
    /// definition applies.
    /// </summary>
    /// <value>The minimum version.</value>
    public BrowserVersion MaximumVersion { get; set; }

    /// <summary>
    /// Gets or sets a collection of the platform names to which this definition applies.
    /// </summary>
    /// <value>The platforms.</value>
    public ISet<string> Platforms
    {
      get { return platforms; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        platforms = value;
      }
    }

    /// <summary>
    /// Gets or sets a collection of the browser flags which this definition adds to matched browsers.
    /// </summary>
    /// <value>The flags to be added to matched browsers.</value>
    public ISet<string> AddFlags
    {
      get { return addFlags; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        addFlags = value;
      }
    }

    /// <summary>
    /// Gets or sets a collection of the browser flags which this definition should remove from matched browsers.
    /// </summary>
    /// <value>The flags to be removed from matched browsers.</value>
    public ISet<string> RemoveFlags
    {
      get { return removeFlags; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        removeFlags = value;
      }
    }

    /// <summary>
    /// Performs a test and returns a value which determines whether the given browser identification
    /// is a match for this definition instance.
    /// </summary>
    /// <param name="browserId">A browser identification instance.</param>
    public bool Matches(BrowserIdentification browserId)
    {
      if(browserId == null)
        throw new ArgumentNullException(nameof(browserId));
      
      if(!DoesPlatformMatch(browserId))
        return false;
      
      if(!DoesBrowserNameMatch(browserId))
        return false;

      if(!SatisfiesMinVersion(browserId))
        return false;

      if(!SatisfiesMaxVersion(browserId))
        return false;

      return true;
    }

    bool DoesPlatformMatch(BrowserIdentification browserId)
    {
      if(Platforms == null || !Platforms.Any())
        return true;

      return Platforms.Any(x => x.Equals(browserId.Platform, StringComparison.InvariantCultureIgnoreCase));
    }

    bool DoesBrowserNameMatch(BrowserIdentification browserId)
    {
      return BrowserNames.Any(x => x.Equals(browserId.Name, StringComparison.InvariantCultureIgnoreCase));
    }

    bool SatisfiesMinVersion(BrowserIdentification browserId)
    {
      if(MinimumVersion == null)
        return true;
      
      return browserId.Version >= MinimumVersion;
    }

    bool SatisfiesMaxVersion(BrowserIdentification browserId)
    {
      if(MaximumVersion == null)
        return true;

      return browserId.Version < MaximumVersion;
    }

    /// <summary>
    /// Determines whether the specified <see cref="FlagsDefinition"/> is equal to the current
    /// <see cref="FlagsDefinition"/>.
    /// </summary>
    /// <param name="other">The <see cref="FlagsDefinition"/> to compare with the current <see cref="FlagsDefinition"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="FlagsDefinition"/> is equal to the current
    /// <see cref="FlagsDefinition"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(FlagsDefinition other)
    {
      if(ReferenceEquals(other, null))
        return false;

      return (MinimumVersion == other.MinimumVersion
              && MaximumVersion == other.MaximumVersion
              && BrowserNames.SetEquals(other.BrowserNames)
              && Platforms.SetEquals(other.Platforms)
              && AddFlags.SetEquals(other.AddFlags)
              && RemoveFlags.SetEquals(other.RemoveFlags));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FlagsDefinition"/> class.
    /// </summary>
    public FlagsDefinition()
    {
      platforms = new HashSet<string>();
      browserNames = new HashSet<string>();
      addFlags = new HashSet<string>();
      removeFlags = new HashSet<string>();
    }
  }
}

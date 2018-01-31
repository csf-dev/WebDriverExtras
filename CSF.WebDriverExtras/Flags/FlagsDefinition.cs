using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Flags
{
  public class FlagsDefinition : IEquatable<FlagsDefinition>
  {
    ISet<string> platforms, browserNames, addFlags, removeFlags;

    public ISet<string> BrowserNames
    {
      get { return browserNames; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        browserNames = value;
      }
    }

    public BrowserVersion MinimumVersion { get; set; }

    public BrowserVersion MaximumVersion { get; set; }

    public ISet<string> Platforms
    {
      get { return platforms; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        platforms = value;
      }
    }

    public ISet<string> AddFlags
    {
      get { return addFlags; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        addFlags = value;
      }
    }

    public ISet<string> RemoveFlags
    {
      get { return removeFlags; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        removeFlags = value;
      }
    }

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

    public FlagsDefinition()
    {
      platforms = new HashSet<string>();
      browserNames = new HashSet<string>();
      addFlags = new HashSet<string>();
      removeFlags = new HashSet<string>();
    }
  }
}

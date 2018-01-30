using System;
using System.Collections.Generic;
using System.Linq;

namespace CSF.WebDriverExtras.Flags
{
  public class FlagsDefinition
  {
    ISet<string> browserNames, flags;

    public ISet<string> BrowserNames
    {
      get { return browserNames; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        browserNames = value;
      }
    }

    public IBrowserVersion MinimumVersion { get; set; }

    public IBrowserVersion MaximumVersion { get; set; }

    public ISet<string> Platforms { get; set; }

    public ISet<string> Flags
    {
      get { return flags; }
      set {
        if(value == null)
          throw new ArgumentNullException(nameof(value));
        flags = value;
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
      
      return browserId.Version.CompareTo(MinimumVersion) >= 0;
    }

    bool SatisfiesMaxVersion(BrowserIdentification browserId)
    {
      if(MaximumVersion == null)
        return true;

      return browserId.Version.CompareTo(MaximumVersion) <= 0;
    }

    public FlagsDefinition()
    {
      browserNames = new HashSet<string>();
      flags = new HashSet<string>();
    }
  }
}

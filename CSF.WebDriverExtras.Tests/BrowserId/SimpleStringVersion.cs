using System;
using CSF.WebDriverExtras.BrowserId;

namespace CSF.WebDriverExtras.Tests.BrowserId
{
  public class SimpleStringVersion : BrowserVersion
  {
    readonly string version;

    public override int CompareTo(BrowserVersion other)
    {
      var strOther = other as SimpleStringVersion;
      if(strOther == null)
        return 1;

      return StringComparer.InvariantCulture.Compare(version, strOther.version);
    }

    public override bool Equals(BrowserVersion other)
    {
      var strOther = other as SimpleStringVersion;
      if(strOther == null)
        return false;

      return version.Equals(strOther.version, StringComparison.InvariantCulture);
    }

    public override int GetHashCode() => version.GetHashCode();

    public SimpleStringVersion(string version)
    {
      if(version == null)
        throw new ArgumentNullException(nameof(version));

      this.version = version;
    }
  }
}

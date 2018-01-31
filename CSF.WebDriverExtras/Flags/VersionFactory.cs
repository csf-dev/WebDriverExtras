using System;
namespace CSF.WebDriverExtras.Flags
{
  public class VersionFactory : ICreatesBrowserVersions
  {
    public BrowserVersion CreateVersion(string versionString) => SemanticVersion.Parse(versionString);
  }
}

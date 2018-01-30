using System;
namespace CSF.WebDriverExtras.Flags
{
  public class VersionFactory : ICreatesBrowserVersions
  {
    public IBrowserVersion CreateVersion(string versionString) => SemanticVersion.Parse(versionString);
  }
}

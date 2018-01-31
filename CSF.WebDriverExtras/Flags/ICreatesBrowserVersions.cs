using System;
namespace CSF.WebDriverExtras.Flags
{
  public interface ICreatesBrowserVersions
  {
    BrowserVersion CreateVersion(string versionString);
  }
}

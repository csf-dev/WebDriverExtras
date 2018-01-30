using System;
namespace CSF.WebDriverExtras.Flags
{
  public interface ICreatesBrowserVersions
  {
    IBrowserVersion CreateVersion(string versionString);
  }
}

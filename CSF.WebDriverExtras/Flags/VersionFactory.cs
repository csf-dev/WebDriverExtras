using System;
namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Default implementation of <see cref="ICreatesBrowserVersions"/>.
  /// </summary>
  public class VersionFactory : ICreatesBrowserVersions
  {
    /// <summary>
    /// Creates and returns a browser version instance.
    /// </summary>
    /// <returns>The version.</returns>
    /// <param name="versionString">The string to parse.</param>
    public BrowserVersion CreateVersion(string versionString) => SemanticVersion.Parse(versionString);
  }
}

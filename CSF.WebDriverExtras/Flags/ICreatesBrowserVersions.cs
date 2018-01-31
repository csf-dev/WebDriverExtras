using System;
namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// A factory service which parses strings and creates a suitable implementation of
  /// <see cref="BrowserVersion"/> from them.
  /// </summary>
  public interface ICreatesBrowserVersions
  {
    /// <summary>
    /// Creates and returns a browser version instance.
    /// </summary>
    /// <returns>The version.</returns>
    /// <param name="versionString">The string to parse.</param>
    BrowserVersion CreateVersion(string versionString);
  }
}

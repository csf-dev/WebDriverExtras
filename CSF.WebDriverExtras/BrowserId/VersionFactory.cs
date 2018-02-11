using System;
namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// Default implementation of <see cref="ICreatesBrowserVersions"/>.
  /// </summary>
  public class VersionFactory : ICreatesBrowserVersions
  {
    /// <summary>
    /// Creates and returns a browser version instance for a given browser.
    /// </summary>
    /// <returns>The version.</returns>
    /// <param name="versionString">The browser version string to parse.</param>
    /// <param name="browserName">The name of the browser for which to create a version.</param>
    /// <param name="requestedVersionString">A string indicating the browser version which was 'requested'.</param>
    public BrowserVersion CreateVersion(string versionString,
                                        string browserName = null,
                                        string requestedVersionString = null)
    {
      if(String.IsNullOrEmpty(versionString))
        return EmptyBrowserVersion.Singleton;

      BrowserVersion output = SemanticVersion.Parse(versionString);
      if(output != null) return output;

      output = DottedNumericVersion.Parse(versionString);
      if(output != null) return output;

      return new UnrecognisedVersion(versionString);
    }
  }
}

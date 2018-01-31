using System;
namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Implementation of <see cref="LocalDriverOptions"/> for Google Chrome.
  /// </summary>
  public class LocalChromeOptions : LocalDriverOptions
  {
    /// <summary>
    /// Gets or sets the filesystem path to the web browser executable.
    /// </summary>
    /// <value>The web browser executable path.</value>
    public string WebBrowserExecutablePath { get; set; }
  }
}

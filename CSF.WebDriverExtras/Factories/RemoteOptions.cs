using System;
namespace CSF.WebDriverExtras.Factories
{
  public class RemoteOptions
  {
    /// <summary>
    /// Gets or sets the desired browser name.
    /// </summary>
    /// <value>The name of the browser.</value>
    public string BrowserName { get; set; }

    /// <summary>
    /// Gets or sets the desired browser version.
    /// </summary>
    /// <value>The browser version.</value>
    public string BrowserVersion { get; set; }

    /// <summary>
    /// Gets or sets the desired platform.
    /// </summary>
    /// <value>The platform.</value>
    public string Platform { get; set; }
  }
}

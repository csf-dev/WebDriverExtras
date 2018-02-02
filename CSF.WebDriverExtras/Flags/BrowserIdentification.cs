using System;
namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Indicates the identification of a web browser.
  /// </summary>
  public class BrowserIdentification
  {
    /// <summary>
    /// Gets the browser name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the platform upon which the browser is running.
    /// </summary>
    /// <value>The platform.</value>
    public string Platform { get; private set; }

    /// <summary>
    /// Gets the browser's version.
    /// </summary>
    /// <value>The version.</value>
    public BrowserVersion Version { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserIdentification"/> class.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="version">Version.</param>
    /// <param name="platform">Platform.</param>
    public BrowserIdentification(string name, BrowserVersion version, string platform)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));
      if(version == null)
        throw new ArgumentNullException(nameof(version));
      if(platform == null)
        throw new ArgumentNullException(nameof(platform));

      Name = name;
      Version = version;
      Platform = platform;
    }
  }
}

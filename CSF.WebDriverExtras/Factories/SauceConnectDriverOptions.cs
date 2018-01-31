using System;
namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Implementation of <see cref="RemoteDriverOptions"/> for Sauce Labs 'Sauce Connect' web driver.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Sauce Labs documentation: https://wiki.saucelabs.com/display/DOCS/Sauce+Connect+Proxy
  /// </para>
  /// </remarks>
  public class SauceConnectDriverOptions : RemoteDriverOptions
  {
    /// <summary>
    /// Gets or sets the Sauce Labs tunnel identifier.
    /// </summary>
    /// <value>The tunnel identifier.</value>
    public string TunnelIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the Sauce Connect username.
    /// </summary>
    /// <value>The Sauce Connect username.</value>
    public string SauceConnectUsername { get; set; }

    /// <summary>
    /// Gets or sets the Sauce Connect API key.
    /// </summary>
    /// <value>The Sauce Connect API key.</value>
    public string SauceConnectApiKey { get; set; }

    /// <summary>
    /// Gets or sets the 'build name' given to Sauce Labs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Loosely this is the name of the test run.  Many test scenarios will execute within a single build name.
    /// The build name groups all of the scenarios which have executed together within the test run.
    /// </para>
    /// </remarks>
    /// <value>The build name passed to Sauce Labs.</value>
    public string SauceLabsBuildName { get; set; }
  }
}

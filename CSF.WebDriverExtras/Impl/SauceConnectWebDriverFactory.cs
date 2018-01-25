using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Impl
{
  /// <summary>
  /// Web driver factory base type designed for integration with Sauce Connect by Sauce Labs:
  /// https://saucelabs.com/
  /// </summary>
  public abstract class SauceConnectWebDriverFactory : RemoteWebDriverFromEnvironmentFactory, IMarksWebDriverWithOutcome
  {
    const string
      TunnelIdCapability = "tunnel-identifier",
      UsernameCapability = "username",
      ApiKeyCapability = "accessKey",
      TestNameCapabilityName = "name",
      BuildNameCapability = "build";

    /// <summary>
    /// Gets the name of the capability which provides the test name.
    /// </summary>
    /// <value>The test name capability.</value>
    public static string TestNameCapability => TestNameCapabilityName;

    /// <summary>
    /// Configures the capabilities desired for the current instance.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected override void ConfigureCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      base.ConfigureCapabilities(caps);
      ConfigureSauceConnectCapabilities(caps);
    }

    /// <summary>
    /// Configures capabilities specific to Sauce Connect.
    /// </summary>
    /// <param name="caps">Caps.</param>
    protected virtual void ConfigureSauceConnectCapabilities(OpenQA.Selenium.Remote.DesiredCapabilities caps)
    {
      caps.SetCapability(TunnelIdCapability, GetSauceTunnelId());
      caps.SetCapability(UsernameCapability, GetSauceUsername());
      caps.SetCapability(ApiKeyCapability, GetSauceAccessKey());
      caps.SetCapability(BuildNameCapability, GetSauceBuildName());
    }

    /// <summary>
    /// Gets the tunnel identifier.
    /// </summary>
    /// <returns>The tunnel identifier.</returns>
    protected abstract string GetSauceTunnelId();

    /// <summary>
    /// Gets the Sauce Connect username.
    /// </summary>
    /// <returns>The sauce username.</returns>
    protected abstract string GetSauceUsername();

    /// <summary>
    /// Gets the Sauce Connect access key.
    /// </summary>
    /// <returns>The sauce access key.</returns>
    protected abstract string GetSauceAccessKey();

    /// <summary>
    /// Gets the Sauce Labs 'build name' (a name for the current test run).
    /// </summary>
    /// <returns>The sauce build name.</returns>
    protected abstract string GetSauceBuildName();
  }
}

using System;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  /// <summary>
  /// A service which creates an approproate proxy instance for a Selenium <c>RemoteWebDriver</c>
  /// and wraps a given driver with that proxy.
  /// </summary>
  public interface IWrapsRemoteDriversInProxies
  {
    /// <summary>
    /// Wraps the given web driver in a proxy and returns that proxy.
    /// </summary>
    /// <returns>The web driver proxy.</returns>
    /// <param name="driver">The web driver to wrap.</param>
    /// <param name="flagsProvider">A service which provides web browser flags.</param>
    IWebDriver WrapWithProxy(RemoteWebDriver driver, IGetsBrowserFlags flagsProvider);
  }
}

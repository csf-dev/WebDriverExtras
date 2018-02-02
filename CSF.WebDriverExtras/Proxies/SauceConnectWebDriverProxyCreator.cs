using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  /// <summary>
  /// A specialisation of <see cref="RemoteWebDriverProxyCreator"/> which creates proxies
  /// suitable for use with Sauce Labs/Sauce Connect.
  /// </summary>
  public class SauceConnectWebDriverProxyCreator : RemoteWebDriverProxyCreator
  {
    /// <summary>
    /// Gets the proxy service instance.
    /// </summary>
    /// <returns>The proxy.</returns>
    /// <param name="driver">The driver to wrap.</param>
    /// <param name="flags">A collection of flags.</param>
    protected override IWebDriver GetProxy(RemoteWebDriver driver, IReadOnlyCollection<string> flags)
      => new SauceConnectRemoteWebDriverProxy(driver, flags, new SauceLabsSuccessFailureGateway());
  }
}

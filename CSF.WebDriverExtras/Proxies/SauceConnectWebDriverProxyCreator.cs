using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.BrowserId;
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
    /// <param name="requestedBrowserVersion">The requested browser version.</param>
    protected override IWebDriver GetProxy(RemoteWebDriver driver,
                                           IReadOnlyCollection<string> flags,
                                           string requestedBrowserVersion)
      => new SauceConnectRemoteWebDriverProxy(driver, flags, new SauceLabsSuccessFailureGateway(), requestedBrowserVersion);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.Proxies.SauceConnectWebDriverProxyCreator"/> class.
    /// </summary>
    public SauceConnectWebDriverProxyCreator() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.Proxies.SauceConnectWebDriverProxyCreator"/> class.
    /// </summary>
    /// <param name="identificationFactory">Identification factory.</param>
    public SauceConnectWebDriverProxyCreator(IGetsBrowserIdentification identificationFactory)
      : base(identificationFactory) {}
  }
}

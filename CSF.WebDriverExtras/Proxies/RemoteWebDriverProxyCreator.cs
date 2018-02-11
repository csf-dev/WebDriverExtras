using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.BrowserId;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  /// <summary>
  /// An implementation of <see cref="IWrapsRemoteDriversInProxies"/> which is suitable for any remote
  /// web driver.
  /// </summary>
  public class RemoteWebDriverProxyCreator : IWrapsRemoteDriversInProxies
  {
    readonly IGetsBrowserIdentification identificationFactory;

    /// <summary>
    /// Wraps the given web driver in a proxy and returns that proxy.
    /// </summary>
    /// <returns>The web driver proxy.</returns>
    /// <param name="driver">The web driver to wrap.</param>
    /// <param name="flagsProvider">A service which provides web browser flags.</param>
    /// <param name="desiredCapabilities">A collection of the originally-requested browser capabilities.</param>
    public IWebDriver WrapWithProxy(RemoteWebDriver driver,
                                    IGetsBrowserFlags flagsProvider,
                                    ICapabilities desiredCapabilities)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var flags = GetFlags(driver, flagsProvider, desiredCapabilities);

      return GetProxy(driver, flags, desiredCapabilities?.Version);
    }

    /// <summary>
    /// Gets the proxy service instance.
    /// </summary>
    /// <returns>The proxy.</returns>
    /// <param name="driver">The driver to wrap.</param>
    /// <param name="flags">A collection of flags.</param>
    /// <param name="requestedBrowserVersion">The requested browser version.</param>
    protected virtual IWebDriver GetProxy(RemoteWebDriver driver,
                                          IReadOnlyCollection<string> flags,
                                          string requestedBrowserVersion)
      => new RemoteWebDriverProxy(driver, flags, requestedBrowserVersion);

    /// <summary>
    /// Gets the browser flags from a service.
    /// </summary>
    /// <returns>The flags.</returns>
    /// <param name="driver">The web driver.</param>
    /// <param name="flagsProvider">A flags provider service.</param>
    /// <param name="desiredCapabilities">The desired capabilities.</param>
    protected virtual IReadOnlyCollection<string> GetFlags(IHasCapabilities driver,
                                                           IGetsBrowserFlags flagsProvider,
                                                           ICapabilities desiredCapabilities)
    {
      var identification = identificationFactory.GetIdentification(driver, desiredCapabilities);
      return flagsProvider?.GetFlags(identification);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.Proxies.RemoteWebDriverProxyCreator"/> class.
    /// </summary>
    public RemoteWebDriverProxyCreator() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.Proxies.RemoteWebDriverProxyCreator"/> class.
    /// </summary>
    /// <param name="identificationFactory">Identification factory.</param>
    public RemoteWebDriverProxyCreator(IGetsBrowserIdentification identificationFactory)
    {
      this.identificationFactory = identificationFactory ?? new BrowserIdentificationFactory();
    }
  }
}

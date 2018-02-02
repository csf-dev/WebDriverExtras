using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Wraps the given web driver in a proxy and returns that proxy.
    /// </summary>
    /// <returns>The web driver proxy.</returns>
    /// <param name="driver">The web driver to wrap.</param>
    /// <param name="flagsProvider">A service which provides web browser flags.</param>
    public IWebDriver WrapWithProxy(RemoteWebDriver driver, IGetsBrowserFlags flagsProvider)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var flags = GetFlags(driver, flagsProvider);

      return GetProxy(driver, flags);
    }

    /// <summary>
    /// Gets the proxy service instance.
    /// </summary>
    /// <returns>The proxy.</returns>
    /// <param name="driver">The driver to wrap.</param>
    /// <param name="flags">A collection of flags.</param>
    protected virtual IWebDriver GetProxy(RemoteWebDriver driver, IReadOnlyCollection<string> flags)
      => new RemoteWebDriverProxy(driver, flags);

    /// <summary>
    /// Gets the browser flags from a service.
    /// </summary>
    /// <returns>The flags.</returns>
    /// <param name="driver">The web driver.</param>
    /// <param name="flagsProvider">A flags provider service.</param>
    protected virtual IReadOnlyCollection<string> GetFlags(IHasCapabilities driver, IGetsBrowserFlags flagsProvider)
      => flagsProvider?.GetFlags(driver);
  }
}

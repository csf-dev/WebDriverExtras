using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Proxies;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Base type for factories which create type which is or derives from
  /// <c>OpenQA.Selenium.Remote.RemoteWebDriver</c>.
  /// </summary>
  /// <remarks>
  /// <para>
  /// As of the current Selenium version, just about all of the web driver implementations available derive
  /// from RemoteWebDriver, so this is used a lot.
  /// </para>
  /// <para>
  /// This type really just holds some boilerplate code which used by a number of other implementations.
  /// </para>
  /// </remarks>
  /// <typeparam name="TOptions">The factory options type used by this factory implementation.</typeparam>
  public abstract class RemoteDriverFactoryBase<TOptions> : ICreatesWebDriverFromOptions
    where TOptions : class,new()
  {
    object ICreatesWebDriverFromOptions.CreateEmptyOptions() => CreateEmptyOptions();

    /// <summary>
    /// Creates an empty instance of <typeparamref name="TOptions"/>.
    /// </summary>
    /// <returns>The empty options.</returns>
    protected virtual TOptions CreateEmptyOptions() => new TOptions();

    IWebDriver ICreatesWebDriver.CreateWebDriver(IDictionary<string, object> requestedCapabilities,
                                                 IGetsBrowserFlags flagsProvider,
                                                 string scenarioName)
      => CreateWebDriver(requestedCapabilities, null, flagsProvider, scenarioName);

    IWebDriver ICreatesWebDriverFromOptions.CreateWebDriver(object options,
                                                            IDictionary<string, object> requestedCapabilities,
                                                            IGetsBrowserFlags flagsProvider,
                                                            string scenarioName)
      => CreateWebDriver(requestedCapabilities, (TOptions) options, flagsProvider, scenarioName);

    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">A collection of requested web driver capabilities.</param>
    /// <param name="options">A factory options instance.</param>
    /// <param name="flagsProvider">A service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">The name for the current test scenario.</param>
    public abstract IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               TOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName);

    /// <summary>
    /// Creates a new remote web driver proxy wrapping the given driver, then returns that proxy.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The wrapping of a web driver with a proxy allows us to extend its functionality.
    /// </para>
    /// </remarks>
    /// <returns>The proxied web driver.</returns>
    /// <param name="driver">A remote web driver.</param>
    /// <param name="flagsProvider">A service which provides browser flags.</param>
    protected virtual IWebDriver WrapWithProxy(RemoteWebDriver driver,
                                               IGetsBrowserFlags flagsProvider)
    {
      var proxyFactory = GetProxyFactory();
      return proxyFactory.WrapWithProxy(driver, flagsProvider);
    }

    /// <summary>
    /// Gets a factory service which wraps remote web drivers in proxies.
    /// </summary>
    /// <returns>The proxy factory.</returns>
    protected virtual IWrapsRemoteDriversInProxies GetProxyFactory()
      => new RemoteWebDriverProxyCreator();
  }
}

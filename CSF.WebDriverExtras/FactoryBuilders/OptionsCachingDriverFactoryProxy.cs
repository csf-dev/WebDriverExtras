using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A proxy type which wraps an instance of <see cref="ICreatesWebDriverFromOptions"/> and implements
  /// <see cref="ICreatesWebDriver"/>.  Used to cache and pass options into the inner factory.
  /// </summary>
  public class OptionsCachingDriverFactoryProxy : ICreatesWebDriver
  {
    readonly object options;
    readonly ICreatesWebDriverFromOptions proxiedFactory;

    /// <summary>
    /// Gets a reference to the wrapped/proxied web driver factory.
    /// </summary>
    /// <value>The proxied factory.</value>
    public ICreatesWebDriver ProxiedFactory => proxiedFactory;

    /// <summary>
    /// Creates and returns a web driver instance.
    /// </summary>
    /// <returns>The web driver.</returns>
    /// <param name="requestedCapabilities">An optional collection of requested web driver capabilities.</param>
    /// <param name="flagsProvider">An optional service which derives a collection of browser flags for the created web driver.</param>
    /// <param name="scenarioName">An optional name for the current test scenario.</param>
    public IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities = null,
                                      IGetsBrowserFlags flagsProvider = null,
                                      string scenarioName = null)
      => proxiedFactory.CreateWebDriver(options, requestedCapabilities, flagsProvider, scenarioName);

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="OptionsCachingDriverFactoryProxy"/> class.
    /// </summary>
    /// <param name="proxiedFactory">The web driver factory to proxy.</param>
    /// <param name="options">The factory options object to pass to the proxied factory.</param>
    public OptionsCachingDriverFactoryProxy(ICreatesWebDriverFromOptions proxiedFactory, object options)
    {
      if(proxiedFactory == null)
        throw new ArgumentNullException(nameof(proxiedFactory));

      this.options = options;
      this.proxiedFactory = proxiedFactory;
    }
  }
}

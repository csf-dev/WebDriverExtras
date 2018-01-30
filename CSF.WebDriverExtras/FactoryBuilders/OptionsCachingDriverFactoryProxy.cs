using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class OptionsCachingDriverFactoryProxy : ICreatesWebDriver
  {
    readonly object options;
    readonly ICreatesWebDriverFromOptions proxiedProvider;

    public ICreatesWebDriver ProxiedProvider => proxiedProvider;

    public IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities = null,
                                      IGetsBrowserFlags flagsProvider = null,
                                      string scenarioName = null)
      => proxiedProvider.CreateWebDriver(options, requestedCapabilities, flagsProvider, scenarioName);

    public OptionsCachingDriverFactoryProxy(ICreatesWebDriverFromOptions proxiedProvider,
                                                      object options)
    {
      if(proxiedProvider == null)
        throw new ArgumentNullException(nameof(proxiedProvider));

      this.options = options;
      this.proxiedProvider = proxiedProvider;
    }
  }
}

using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.Proxies;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
  public abstract class RemoteDriverFactoryBase<TOptions> : ICreatesWebDriverFromOptions
    where TOptions : class,new()
  {
    object ICreatesWebDriverFromOptions.CreateEmptyOptions() => CreateEmptyOptions();

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

    public abstract IWebDriver CreateWebDriver(IDictionary<string,object> requestedCapabilities,
                                               TOptions options,
                                               IGetsBrowserFlags flagsProvider,
                                               string scenarioName);

    protected virtual IWebDriver CreateProxy(RemoteWebDriver driver,
                                             IGetsBrowserFlags flagsProvider)
    {
      var proxyFactory = GetProxyFactory();
      return proxyFactory.WrapWithProxy(driver, flagsProvider);
    }

    protected virtual IWrapsRemoteDriversInProxies GetProxyFactory()
      => new RemoteWebDriverProxyCreator();
  }
}

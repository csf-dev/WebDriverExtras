using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  public class RemoteWebDriverProxyCreator : IWrapsRemoteDriversInProxies
  {
    public IWebDriver WrapWithProxy(RemoteWebDriver driver, IGetsBrowserFlags flagsProvider)
    {
      if(driver == null)
        throw new ArgumentNullException(nameof(driver));

      var flags = GetFlags(driver, flagsProvider);

      return GetProxy(driver, flags);
    }

    protected virtual IWebDriver GetProxy(RemoteWebDriver driver, IReadOnlyCollection<string> flags)
      => new RemoteWebDriverProxy(driver, flags);

    protected virtual IReadOnlyCollection<string> GetFlags(IHasCapabilities driver, IGetsBrowserFlags flagsProvider)
      => flagsProvider?.GetFlags(driver);
  }
}

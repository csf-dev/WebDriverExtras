using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  public class SauceConnectWebDriverProxyCreator : RemoteWebDriverProxyCreator
  {
    protected override IWebDriver GetProxy(RemoteWebDriver driver, IReadOnlyCollection<string> flags)
      => new SauceConnectWebDriverProxy(driver, flags, new SauceLabsSuccessFailureGateway());
  }
}

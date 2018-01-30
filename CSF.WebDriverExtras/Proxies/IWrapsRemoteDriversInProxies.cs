using System;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  public interface IWrapsRemoteDriversInProxies
  {
    IWebDriver WrapWithProxy(RemoteWebDriver driver, IGetsBrowserFlags flagsProvider);
  }
}

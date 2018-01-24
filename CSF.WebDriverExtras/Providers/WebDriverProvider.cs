using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Providers
{
  public class WebDriverProvider
    : IProvidesWebDriver, IHasRequestedCapabilities, IHasBrowserInfo, IHasPlatformInfo
  {
    readonly string browserName, browserVersion, platform;
    readonly IReadOnlyDictionary<string, object> requestedCapabilities;
    readonly IWebDriver webDriver;

    public string BrowserName => browserName;

    public string BrowserVersion => browserVersion;

    public string Platform => platform;

    public IReadOnlyDictionary<string, object> RequestedCapabilities => requestedCapabilities;

    public IWebDriver WebDriver => webDriver;

    public bool HasRequestedCapability(string capability) => RequestedCapabilities.ContainsKey(capability);

    public WebDriverProvider(IWebDriver webDriver,
                             string browserName,
                             string browserVersion,
                             string platform,
                             IDictionary<string, object> requestedCapabilities)
    {
      if(requestedCapabilities == null)
        throw new ArgumentNullException(nameof(requestedCapabilities));
      if(platform == null)
        throw new ArgumentNullException(nameof(platform));
      if(browserVersion == null)
        throw new ArgumentNullException(nameof(browserVersion));
      if(browserName == null)
        throw new ArgumentNullException(nameof(browserName));
      if(webDriver == null)
        throw new ArgumentNullException(nameof(webDriver));


      this.webDriver = webDriver;
      this.browserName = browserName;
      this.browserVersion = browserVersion;
      this.platform = platform;
      this.requestedCapabilities = new ReadOnlyDictionary<string,object>(requestedCapabilities);
    }
  }
}

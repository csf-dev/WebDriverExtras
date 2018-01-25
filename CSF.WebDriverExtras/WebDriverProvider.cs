using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  public class WebDriverProvider : IProvidesWebDriver, ICanSendSuccessFailureInfo
  {
    readonly string browserName, browserVersion, platform;
    readonly IDictionary<string, object> requestedCapabilities;
    readonly object options;
    readonly IWebDriver webDriver;
    readonly IGetsBrowserFlags flagsProvider;
    readonly ISuccessAndFailureGateway successFailureGateway;

    public string BrowserName => browserName;

    public string BrowserVersion => browserVersion;

    public string Platform => platform;

    public IReadOnlyDictionary<string, object> RequestedCapabilities
      => new ReadOnlyDictionary<string,object>(requestedCapabilities);

    public IWebDriver WebDriver => webDriver;

    public IReadOnlyCollection<string> Flags => flagsProvider.GetFlags(requestedCapabilities, options);

    public bool HasRequestedCapability(string capability) => requestedCapabilities.ContainsKey(capability);

    public bool HasFlag(string flag) => Flags.Contains(flag);

    public void SendSuccess() => successFailureGateway.SendSuccess(WebDriver);

    public void SendFailure() => successFailureGateway.SendFailure(WebDriver);

    public WebDriverProvider(IWebDriver webDriver,
                             string browserName,
                             string browserVersion,
                             string platform,
                             IDictionary<string, object> requestedCapabilities,
                             object options,
                             IGetsBrowserFlags flagsProvider = null,
                             ISuccessAndFailureGateway successFailureGateway = null)
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
      this.requestedCapabilities = requestedCapabilities;
      this.options = options;
      this.flagsProvider = flagsProvider ?? new EmptyFlagsProvider();
      this.successFailureGateway = successFailureGateway ?? new NoOpSuccessAndFailureGateway();
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Providers
{
  public class WebDriverProviderWithFlags
    : IProvidesWebDriver, IHasRequestedCapabilities, IHasBrowserInfo, IHasPlatformInfo, IHasBrowserFlags
  {
    readonly WebDriverProvider provider;
    IReadOnlyCollection<string> flags;

    public IWebDriver WebDriver => provider.WebDriver;

    public IReadOnlyDictionary<string, object> RequestedCapabilities => provider.RequestedCapabilities;

    public string BrowserName => provider.BrowserName;

    public string BrowserVersion => provider.BrowserVersion;

    public string Platform => provider.Platform;

    public IReadOnlyCollection<string> Flags => flags;

    public bool HasRequestedCapability(string capability) => provider.HasRequestedCapability(capability);

    public bool HasFlag(string flag) => Flags.Contains(flag);

    public WebDriverProviderWithFlags(WebDriverProvider provider, ICollection<string> flags)
    {
      if(provider == null)
        throw new ArgumentNullException(nameof(provider));
      if(flags == null)
        throw new ArgumentNullException(nameof(flags));

      this.provider = provider;
      this.flags = flags.ToArray();
    }
  }
}

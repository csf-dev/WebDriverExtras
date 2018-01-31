using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Flags
{
  public class BrowserIdentificationFactory : IGetsBrowserIdentification
  {
    readonly ICreatesBrowserVersions versionFactory;

    public BrowserIdentification GetIdentification(IHasCapabilities webDriver)
    {
      if(webDriver == null) return null;

      return new BrowserIdentification(webDriver.Capabilities.BrowserName,
                                       versionFactory.CreateVersion(webDriver.Capabilities.Version),
                                       webDriver.Capabilities.Platform.ToString());
    }

    public BrowserIdentificationFactory() : this(null) {}

    public BrowserIdentificationFactory(ICreatesBrowserVersions versionFactory)
    {
      this.versionFactory = versionFactory ?? new VersionFactory();
    }
  }
}

using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// Default implementation of <see cref="IGetsBrowserIdentification"/>.
  /// </summary>
  public class BrowserIdentificationFactory : IGetsBrowserIdentification
  {
    readonly ICreatesBrowserVersions versionFactory;

    /// <summary>
    /// Gets a browser identification instance from the given web driver.
    /// </summary>
    /// <returns>The identification.</returns>
    /// <param name="webDriver">Web driver.</param>
    public BrowserIdentification GetIdentification(IHasCapabilities webDriver)
    {
      if(webDriver == null) return BrowserIdentification.UnidentifiedBrowser;

      return new BrowserIdentification(webDriver.Capabilities.BrowserName,
                                       versionFactory.CreateVersion(webDriver.Capabilities.Version),
                                       webDriver.Capabilities.Platform.ToString());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserIdentificationFactory"/> class.
    /// </summary>
    public BrowserIdentificationFactory() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="BrowserIdentificationFactory"/> class.
    /// </summary>
    /// <param name="versionFactory">A factory service which creates browser versions.</param>
    public BrowserIdentificationFactory(ICreatesBrowserVersions versionFactory)
    {
      this.versionFactory = versionFactory ?? new VersionFactory();
    }
  }
}

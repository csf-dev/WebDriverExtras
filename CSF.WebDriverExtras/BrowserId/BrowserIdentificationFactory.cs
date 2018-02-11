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
      return GetIdentification(webDriver, null);
    }

    /// <summary>
    /// Gets a browser identification instance from the given web driver.
    /// </summary>
    /// <returns>The identification.</returns>
    /// <param name="webDriver">A web drivr which has capabilities.</param>
    /// <param name="desiredCapabilities">The originally-requested capabilities.</param>
    public BrowserIdentification GetIdentification(IHasCapabilities webDriver, ICapabilities desiredCapabilities)
    {
      if(webDriver == null) return BrowserIdentification.UnidentifiedBrowser;

      var platform = webDriver.Capabilities.Platform.ToString();
      var browserName = webDriver.Capabilities.BrowserName;
      var actualVersion = webDriver.Capabilities.Version;
      var requestedVersion = desiredCapabilities?.Version;

      var version = versionFactory.CreateVersion(actualVersion, browserName, requestedVersion);
      return new BrowserIdentification(browserName, version, platform);
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

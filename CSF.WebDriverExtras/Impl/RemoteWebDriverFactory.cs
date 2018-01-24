using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Impl
{
  /// <summary>
  /// Implementation of <see cref="IWebDriverFactory"/> which gets a web driver for a remote web browser.
  /// </summary>
  public class RemoteWebDriverFactory : IWebDriverFactory
  {

    /// <summary>
    /// Gets the name of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser name.</returns>
    public virtual string GetBrowserName() => BrowserName;

    /// <summary>
    /// Gets the version of the web browser that this factory will create.
    /// </summary>
    /// <returns>The browser version.</returns>
    public virtual string GetBrowserVersion() => BrowserVersion;

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public virtual IWebDriver GetWebDriver()
    {
      return GetWebDriver(null);
    }

    /// <summary>
    /// Gets the web driver.
    /// </summary>
    /// <returns>The web driver.</returns>
    public IWebDriver GetWebDriver(IDictionary<string,object> capabilities)
    {
      var baseCapabilities = GetCapabilities();
      var uri = GetRemoteUri();

      if(capabilities != null)
      {
        foreach(var cap in capabilities)
        {
          baseCapabilities.SetCapability(cap.Key, cap.Value);
        }
      }

      var timeout = GetTimeout();
      return new RemoteWebDriver(uri, baseCapabilities, timeout);
    }

    /// <summary>
    /// Gets the timeout.
    /// </summary>
    /// <returns>The timeout.</returns>
    protected virtual TimeSpan GetTimeout()
    {
      return TimeSpan.FromSeconds(CommandTimeoutSeconds);
    }

    /// <summary>
    /// Sets a desired capability key/value pair into the given capabilities instance, but only if the capability
    /// value is not <c>null</c>.
    /// </summary>
    /// <param name="caps">The capabilities instance to modify.</param>
    /// <param name="name">The capability name.</param>
    /// <param name="value">The capability value.</param>
    protected virtual void SetCapabilityIfNotNull(DesiredCapabilities caps, string name, string value)
    {
      if(value == null)
        return;

      caps.SetCapability(name, value);
    }

    /// <summary>
    /// Gets the URI to the remote web driver.
    /// </summary>
    /// <returns>The remote URI.</returns>
    protected virtual Uri GetRemoteUri()
    {
      return new Uri(RemoteAddress);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverFactory.Impl.RemoteWebDriverFactory"/> class.
    /// </summary>
    public RemoteWebDriverFactory()
    {
      BrowserName = "Chrome";
      RemoteAddress = ;
      CommandTimeoutSeconds = 60;
    }
  }
}

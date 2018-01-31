using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  /// <summary>
  /// A type which proxies/wraps a given Selenium <c>RemoteWebDriver</c> and allows us to add additional
  /// functionality/interfaces to that instance, whilst still supporting all of Selenium's underlying
  /// functionality.
  /// </summary>
  public class RemoteWebDriverProxy : IHasFlags,
  #region Selenium interfaces
  IWebDriver, IDisposable, ISearchContext, IJavaScriptExecutor, IFindsById,
  IFindsByClassName, IFindsByLinkText, IFindsByName, IFindsByTagName, IFindsByXPath,
  IFindsByPartialLinkText, IFindsByCssSelector, ITakesScreenshot, IHasInputDevices,
  IHasCapabilities, IHasWebStorage, IHasLocationContext, IHasApplicationCache,
  IAllowsFileDetection, IHasSessionId, IActionExecutor
  #endregion
  {
    #region fields

    readonly RemoteWebDriver proxiedDriver;
    readonly IReadOnlyCollection<string> flags;

    #endregion

    #region properties

    /// <summary>
    /// Gets the wrapped/proxied driver.
    /// </summary>
    /// <value>The proxied driver.</value>
    protected RemoteWebDriver ProxiedDriver => proxiedDriver;

    #endregion

    #region Selenium built-in functionality

    IApplicationCache IHasApplicationCache.ApplicationCache => proxiedDriver.ApplicationCache;

    ICapabilities IHasCapabilities.Capabilities => proxiedDriver.Capabilities;

    string IWebDriver.CurrentWindowHandle => proxiedDriver.CurrentWindowHandle;

    IFileDetector IAllowsFileDetection.FileDetector
    {
      get { return proxiedDriver.FileDetector; }
      set { proxiedDriver.FileDetector = value; }
    }

    bool IHasApplicationCache.HasApplicationCache => proxiedDriver.HasApplicationCache;

    bool IHasLocationContext.HasLocationContext => proxiedDriver.HasLocationContext;

    bool IHasWebStorage.HasWebStorage => proxiedDriver.HasWebStorage;

    bool IActionExecutor.IsActionExecutor => proxiedDriver.IsActionExecutor;

    IKeyboard IHasInputDevices.Keyboard => proxiedDriver.Keyboard;

    ILocationContext IHasLocationContext.LocationContext => proxiedDriver.LocationContext;

    IMouse IHasInputDevices.Mouse => proxiedDriver.Mouse;

    string IWebDriver.PageSource => proxiedDriver.PageSource;

    SessionId IHasSessionId.SessionId => proxiedDriver.SessionId;

    string IWebDriver.Title => proxiedDriver.Title;

    string IWebDriver.Url
    {
      get { return proxiedDriver.Url; }
      set { proxiedDriver.Url = value; }
    }

    IWebStorage IHasWebStorage.WebStorage => proxiedDriver.WebStorage;

    ReadOnlyCollection<string> IWebDriver.WindowHandles => proxiedDriver.WindowHandles;

    void IWebDriver.Close() => proxiedDriver.Close();

    void IDisposable.Dispose() => proxiedDriver.Dispose();

    object IJavaScriptExecutor.ExecuteAsyncScript(string script, params object[] args)
      => proxiedDriver.ExecuteAsyncScript(script, args);

    object IJavaScriptExecutor.ExecuteScript(string script, params object[] args)
      => proxiedDriver.ExecuteScript(script, args);

    IWebElement ISearchContext.FindElement(By by) => proxiedDriver.FindElement(by);

    IWebElement IFindsByClassName.FindElementByClassName(string className)
      => proxiedDriver.FindElementByClassName(className);

    IWebElement IFindsByCssSelector.FindElementByCssSelector(string cssSelector)
     => proxiedDriver.FindElementByCssSelector(cssSelector);

    IWebElement IFindsById.FindElementById(string id)
     => proxiedDriver.FindElementById(id);

    IWebElement IFindsByLinkText.FindElementByLinkText(string linkText)
     => proxiedDriver.FindElementByLinkText(linkText);

    IWebElement IFindsByName.FindElementByName(string name)
     => proxiedDriver.FindElementByName(name);

    IWebElement IFindsByPartialLinkText.FindElementByPartialLinkText(string partialLinkText)
     => proxiedDriver.FindElementByPartialLinkText(partialLinkText);

    IWebElement IFindsByTagName.FindElementByTagName(string tagName)
     => proxiedDriver.FindElementByTagName(tagName);

    IWebElement IFindsByXPath.FindElementByXPath(string xpath)
     => proxiedDriver.FindElementByXPath(xpath);

    ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
     => proxiedDriver.FindElements(by);

    ReadOnlyCollection<IWebElement> IFindsByClassName.FindElementsByClassName(string className)
     => proxiedDriver.FindElementsByClassName(className);

    ReadOnlyCollection<IWebElement> IFindsByCssSelector.FindElementsByCssSelector(string cssSelector)
     => proxiedDriver.FindElementsByCssSelector(cssSelector);

    ReadOnlyCollection<IWebElement> IFindsById.FindElementsById(string id)
     => proxiedDriver.FindElementsById(id);

    ReadOnlyCollection<IWebElement> IFindsByLinkText.FindElementsByLinkText(string linkText)
     => proxiedDriver.FindElementsByLinkText(linkText);

    ReadOnlyCollection<IWebElement> IFindsByName.FindElementsByName(string name)
     => proxiedDriver.FindElementsByName(name);

    ReadOnlyCollection<IWebElement> IFindsByPartialLinkText.FindElementsByPartialLinkText(string partialLinkText)
     => proxiedDriver.FindElementsByPartialLinkText(partialLinkText);

    ReadOnlyCollection<IWebElement> IFindsByTagName.FindElementsByTagName(string tagName)
     => proxiedDriver.FindElementsByTagName(tagName);

    ReadOnlyCollection<IWebElement> IFindsByXPath.FindElementsByXPath(string xpath)
     => proxiedDriver.FindElementsByXPath(xpath);

    Screenshot ITakesScreenshot.GetScreenshot() => proxiedDriver.GetScreenshot();

    IOptions IWebDriver.Manage() => proxiedDriver.Manage();

    INavigation IWebDriver.Navigate() => proxiedDriver.Navigate();

    void IActionExecutor.PerformActions(IList<ActionSequence> actionSequenceList)
     => proxiedDriver.PerformActions(actionSequenceList);

    void IWebDriver.Quit() => proxiedDriver.Quit();

    void IActionExecutor.ResetInputState() => proxiedDriver.ResetInputState();

    ITargetLocator IWebDriver.SwitchTo() => proxiedDriver.SwitchTo();

    /// <summary>
    /// Gets a collection of all of the flags associated with the current driver.
    /// </summary>
    /// <returns>The flags.</returns>
    #endregion

    #region WebDriverExtras functionality

    public IReadOnlyCollection<string> GetFlags() => flags;

    /// <summary>
    /// Gets a value indicating whether or not the current driver has a given flag.
    /// </summary>
    /// <returns>
    /// <c>true</c>, if the flag is present on the driver, <c>false</c> otherwise.</returns>
    /// <param name="flag">A flag name.</param>
    public bool HasFlag(string flag) => flags.Contains(flag);

    /// <summary>
    /// Analyses an ordered collection of web driver flags and returns the first of which that is present
    /// upon the current web driver.  Returns <c>null</c> if none of the provided flags were present.
    /// </summary>
    /// <returns>The first flag present on this driver.</returns>
    /// <param name="flags">A collection of flags.</param>
    public string GetFirstFlagPresent(params string[] flags)
      => GetFirstFlagPresent((IList<string>) flags ?? new string[0]);

    /// <summary>
    /// Analyses an ordered collection of web driver flags and returns the first of which that is present
    /// upon the current web driver.  Returns <c>null</c> if none of the provided flags were present.
    /// </summary>
    /// <returns>The first flag present on this driver.</returns>
    /// <param name="flags">A collection of flags.</param>
    public string GetFirstFlagPresent(IList<string> flags)
    {
      if(flags == null)
        throw new ArgumentNullException(nameof(flags));

      return flags.FirstOrDefault(HasFlag);
    }

    #endregion

    #region constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoteWebDriverProxy"/> class.
    /// </summary>
    /// <param name="proxiedDriver">The proxied/wrapped web driver.</param>
    /// <param name="flags">A collection of the browser flags for the driver.</param>
    public RemoteWebDriverProxy(RemoteWebDriver proxiedDriver,
                                ICollection<string> flags = null)
    {
      if(proxiedDriver == null)
        throw new ArgumentNullException(nameof(proxiedDriver));

      this.proxiedDriver = proxiedDriver;
      this.flags = new ReadOnlyCollection<string>(flags?.ToList() ?? Enumerable.Empty<string>().ToList());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoteWebDriverProxy"/> class.
    /// </summary>
    /// <param name="proxiedDriver">The proxied/wrapped web driver.</param>
    /// <param name="flags">A collection of the browser flags for the driver.</param>
    public RemoteWebDriverProxy(RemoteWebDriver proxiedDriver,
                                IReadOnlyCollection<string> flags)
    {
      if(proxiedDriver == null)
        throw new ArgumentNullException(nameof(proxiedDriver));

      this.proxiedDriver = proxiedDriver;
      this.flags = flags ?? new string[0];
    }

    #endregion
  }
}

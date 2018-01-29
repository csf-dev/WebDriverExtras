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
  public class RemoteWebDriverProxy : 
  #region Selenium interfaces
  IWebDriver, IDisposable, ISearchContext, IJavaScriptExecutor, IFindsById,
  IFindsByClassName, IFindsByLinkText, IFindsByName, IFindsByTagName, IFindsByXPath,
  IFindsByPartialLinkText, IFindsByCssSelector, ITakesScreenshot, IHasInputDevices,
  IHasCapabilities, IHasWebStorage, IHasLocationContext, IHasApplicationCache,
  IAllowsFileDetection, IHasSessionId, IActionExecutor,
  #endregion
  #region WebDriverExtras interfaces
  IHasFlags
  #endregion
  {
    #region fields

    readonly RemoteWebDriver proxiedDriver;
    readonly IReadOnlyCollection<string> flags;

    #endregion

    #region properties

    protected RemoteWebDriver ProxiedDriver => proxiedDriver;

    #endregion

    #region Selenium built-in functionality

    public IApplicationCache ApplicationCache => proxiedDriver.ApplicationCache;

    public ICapabilities Capabilities => proxiedDriver.Capabilities;

    public string CurrentWindowHandle => proxiedDriver.CurrentWindowHandle;

    public IFileDetector FileDetector
    {
      get { return proxiedDriver.FileDetector; }
      set { proxiedDriver.FileDetector = value; }
    }

    public bool HasApplicationCache => proxiedDriver.HasApplicationCache;

    public bool HasLocationContext => proxiedDriver.HasLocationContext;

    public bool HasWebStorage => proxiedDriver.HasWebStorage;

    public bool IsActionExecutor => proxiedDriver.IsActionExecutor;

    public IKeyboard Keyboard => proxiedDriver.Keyboard;

    public ILocationContext LocationContext => proxiedDriver.LocationContext;

    public IMouse Mouse => proxiedDriver.Mouse;

    public string PageSource => proxiedDriver.PageSource;

    public SessionId SessionId => proxiedDriver.SessionId;

    public string Title => proxiedDriver.Title;

    public string Url
    {
      get { return proxiedDriver.Url; }
      set { proxiedDriver.Url = value; }
    }

    public IWebStorage WebStorage => proxiedDriver.WebStorage;

    public ReadOnlyCollection<string> WindowHandles => proxiedDriver.WindowHandles;

    public void Close() => proxiedDriver.Close();

    public void Dispose() => proxiedDriver.Dispose();

    public object ExecuteAsyncScript(string script, params object[] args)
      => proxiedDriver.ExecuteAsyncScript(script, args);

    public object ExecuteScript(string script, params object[] args)
      => proxiedDriver.ExecuteScript(script, args);

    public IWebElement FindElement(By by) => proxiedDriver.FindElement(by);

    public IWebElement FindElementByClassName(string className)
      => proxiedDriver.FindElementByClassName(className);

    public IWebElement FindElementByCssSelector(string cssSelector)
     => proxiedDriver.FindElementByCssSelector(cssSelector);

    public IWebElement FindElementById(string id)
     => proxiedDriver.FindElementById(id);

    public IWebElement FindElementByLinkText(string linkText)
     => proxiedDriver.FindElementByLinkText(linkText);

    public IWebElement FindElementByName(string name)
     => proxiedDriver.FindElementByName(name);

    public IWebElement FindElementByPartialLinkText(string partialLinkText)
     => proxiedDriver.FindElementByPartialLinkText(partialLinkText);

    public IWebElement FindElementByTagName(string tagName)
     => proxiedDriver.FindElementByTagName(tagName);

    public IWebElement FindElementByXPath(string xpath)
     => proxiedDriver.FindElementByXPath(xpath);

    public ReadOnlyCollection<IWebElement> FindElements(By by)
     => proxiedDriver.FindElements(by);

    public ReadOnlyCollection<IWebElement> FindElementsByClassName(string className)
     => proxiedDriver.FindElementsByClassName(className);

    public ReadOnlyCollection<IWebElement> FindElementsByCssSelector(string cssSelector)
     => proxiedDriver.FindElementsByCssSelector(cssSelector);

    public ReadOnlyCollection<IWebElement> FindElementsById(string id)
     => proxiedDriver.FindElementsById(id);

    public ReadOnlyCollection<IWebElement> FindElementsByLinkText(string linkText)
     => proxiedDriver.FindElementsByLinkText(linkText);

    public ReadOnlyCollection<IWebElement> FindElementsByName(string name)
     => proxiedDriver.FindElementsByName(name);

    public ReadOnlyCollection<IWebElement> FindElementsByPartialLinkText(string partialLinkText)
     => proxiedDriver.FindElementsByPartialLinkText(partialLinkText);

    public ReadOnlyCollection<IWebElement> FindElementsByTagName(string tagName)
     => proxiedDriver.FindElementsByTagName(tagName);

    public ReadOnlyCollection<IWebElement> FindElementsByXPath(string xpath)
     => proxiedDriver.FindElementsByXPath(xpath);

    public Screenshot GetScreenshot() => proxiedDriver.GetScreenshot();

    public IOptions Manage() => proxiedDriver.Manage();

    public INavigation Navigate() => proxiedDriver.Navigate();

    public void PerformActions(IList<ActionSequence> actionSequenceList)
     => proxiedDriver.PerformActions(actionSequenceList);

    public void Quit() => proxiedDriver.Quit();

    public void ResetInputState() => proxiedDriver.ResetInputState();

    public ITargetLocator SwitchTo() => proxiedDriver.SwitchTo();

    #endregion

    #region WebDriverExtras functionality

    public IReadOnlyCollection<string> GetFlags() => flags;

    public bool HasFlag(string flag) => flags.Contains(flag);

    #endregion

    #region constructor

    public RemoteWebDriverProxy(RemoteWebDriver proxiedDriver,
                                ICollection<string> flags = null)
    {
      if(proxiedDriver == null)
        throw new ArgumentNullException(nameof(proxiedDriver));

      this.proxiedDriver = proxiedDriver;
      this.flags = new ReadOnlyCollection<string>(flags?.ToList() ?? Enumerable.Empty<string>().ToList());
    }

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

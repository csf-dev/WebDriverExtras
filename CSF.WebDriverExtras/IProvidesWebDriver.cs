using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  public interface IProvidesWebDriver
  {
    IWebDriver WebDriver { get; }

    string Platform { get; }

    string BrowserName { get; }

    string BrowserVersion { get; }

    IReadOnlyCollection<string> Flags { get; }

    IReadOnlyDictionary<string,object> RequestedCapabilities { get; }

    bool HasFlag(string flag);

    bool HasRequestedCapability(string capability);
  }
}

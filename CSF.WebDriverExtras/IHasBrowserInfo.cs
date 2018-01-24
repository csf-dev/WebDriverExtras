using System;
namespace CSF.WebDriverExtras
{
  public interface IHasBrowserInfo : IProvidesWebDriver
  {
    string BrowserName { get; }

    string BrowserVersion { get; }
  }
}

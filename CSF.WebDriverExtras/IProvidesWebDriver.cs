using System;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  public interface IProvidesWebDriver
  {
    IWebDriver WebDriver { get; }
  }
}

using System;
namespace CSF.WebDriverExtras
{
  public interface IHasPlatformInfo : IProvidesWebDriver
  {
    string Platform { get; }
  }
}

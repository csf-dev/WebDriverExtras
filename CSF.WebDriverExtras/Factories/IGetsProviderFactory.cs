using System;
namespace CSF.WebDriverExtras.Factories
{
  public interface IGetsProviderFactory
  {
    ICreatesWebDriverProviders GetWebDriverProviderFactory();
  }
}

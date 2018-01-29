using System;
namespace CSF.WebDriverExtras
{
  public static class Get
  {
    static readonly FactoryBuilders.WebDriverFactorySource factorySource;

    public static ICreatesWebDriver WebDriverFactory()
      => factorySource.GetWebDriverFactory();

    static Get()
    {
      factorySource = new FactoryBuilders.WebDriverFactorySource();
    }
  }
}

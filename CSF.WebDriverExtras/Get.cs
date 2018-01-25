using System;
namespace CSF.WebDriverExtras
{
  public static class Get
  {
    static readonly FactoryBuilders.WebDriverProviderFactorySource factorySource;

    public static ICreatesWebDriverProviders WebDriverProviderFactory()
      => factorySource.GetWebDriverProviderFactory();

    static Get()
    {
      factorySource = new FactoryBuilders.WebDriverProviderFactorySource();
    }
  }
}

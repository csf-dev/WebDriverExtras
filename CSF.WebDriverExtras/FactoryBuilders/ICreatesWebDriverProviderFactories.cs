using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  public interface ICreatesWebDriverProviderFactories
  {
    ICreatesWebDriverProviders GetFactory(string assemblyQualifiedTypeName);
  }
}

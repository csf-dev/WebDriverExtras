using System;
namespace CSF.WebDriverExtras.Config
{
  public interface ICreatesWebDriverProviderFactories
  {
    ICreatesWebDriverProviders GetFactory(string assemblyQualifiedTypeName);
  }
}

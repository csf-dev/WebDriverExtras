using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  public interface ICreatesWebDriverFactory
  {
    ICreatesWebDriver GetFactory(string assemblyQualifiedTypeName);
  }
}

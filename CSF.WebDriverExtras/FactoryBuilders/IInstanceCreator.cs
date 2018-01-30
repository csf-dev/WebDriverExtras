using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  public interface IInstanceCreator
  {
    object CreateInstance(Type type);
  }
}

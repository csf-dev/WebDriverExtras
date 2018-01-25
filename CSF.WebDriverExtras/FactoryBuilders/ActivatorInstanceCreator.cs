using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class ActivatorInstanceCreator : IInstanceCreator
  {
    public object CreateInstance(Type type) => Activator.CreateInstance(type);
  }
}

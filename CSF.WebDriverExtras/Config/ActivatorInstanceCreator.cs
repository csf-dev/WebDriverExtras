using System;
namespace CSF.WebDriverExtras.Config
{
  public class ActivatorInstanceCreator : IInstanceCreator
  {
    public object CreateInstance(Type type) => Activator.CreateInstance(type);
  }
}

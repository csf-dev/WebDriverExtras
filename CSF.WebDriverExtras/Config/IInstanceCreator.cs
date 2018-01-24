using System;
namespace CSF.WebDriverExtras.Config
{
  public interface IInstanceCreator
  {
    object CreateInstance(Type type);
  }
}

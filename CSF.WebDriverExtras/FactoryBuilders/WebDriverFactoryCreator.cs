using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  public class WebDriverFactoryCreator : ICreatesWebDriverFactory
  {
    readonly IInstanceCreator instanceCreator;

    public ICreatesWebDriver GetFactory(string assemblyQualifiedTypeName)
    {
      if(String.IsNullOrEmpty(assemblyQualifiedTypeName))
        return null;
      
      var type = GetFactoryType(assemblyQualifiedTypeName);
      if(type == null)
        return null;

      return GetFactory(type);
    }


    Type GetFactoryType(string assemblyQualifiedTypeName)
    {
      try
      {
        return Type.GetType(assemblyQualifiedTypeName);
      }
      catch(Exception)
      {
        // We're dealing with user data from the config, bad config just means no factory type found
        return null;
      }
    }

    ICreatesWebDriver GetFactory(Type type)
    {
      object factory;

      try
      {
        factory = instanceCreator.CreateInstance(type);
      }
      catch(Exception)
      {
        // If there was a problem creating the factory just squelch the error and return null for no factory.
        return null;
      }

      return factory as ICreatesWebDriver;
    }

    public WebDriverFactoryCreator(IInstanceCreator instanceCreator)
    {
      if(instanceCreator == null)
        throw new ArgumentNullException(nameof(instanceCreator));

      this.instanceCreator = instanceCreator;
    }
  }
}

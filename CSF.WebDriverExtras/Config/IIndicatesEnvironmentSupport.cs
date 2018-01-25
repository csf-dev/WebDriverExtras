using System;
namespace CSF.WebDriverExtras.Config
{
  public interface IIndicatesEnvironmentSupport : IGetsFactoryConfiguration
  {
    bool EnvironmentVariableSupportEnabled { get; }

    string GetEnvironmentVariablePrefix();
  }
}

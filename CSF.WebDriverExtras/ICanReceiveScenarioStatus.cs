using System;
namespace CSF.WebDriverExtras
{
  public interface ICanReceiveScenarioStatus
  {
    void MarkScenarioAsSuccess();

    void MarkScenarioAsFailure();
  }
}

using System;
namespace CSF.WebDriverExtras
{
  /// <summary>
  /// Indicates that a web driver can receive information about whether a test scenario was a success or a failure.
  /// </summary>
  public interface ICanReceiveScenarioStatus
  {
    /// <summary>
    /// Sends information to the web driver indicating that the current test scenario was a success.
    /// </summary>
    void MarkScenarioAsSuccess();

    /// <summary>
    /// Sends information to the web driver indicating that the current test scenario was a failure.
    /// </summary>
    void MarkScenarioAsFailure();
  }
}

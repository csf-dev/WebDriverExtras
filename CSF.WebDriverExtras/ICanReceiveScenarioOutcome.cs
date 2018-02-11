using System;
namespace CSF.WebDriverExtras
{
  /// <summary>
  /// Indicates that a web driver can receive information about whether a test scenario was a success or a failure.
  /// </summary>
  public interface ICanReceiveScenarioOutcome
  {
    /// <summary>
    /// Sends information to the web driver indicating the outcome of the current test scenario.
    /// </summary>
    /// <param name="success">If set to <c>true</c> then the scenario was a success; otherwise failure.</param>
    void MarkScenarioWithOutcome(bool success);

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

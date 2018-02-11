using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  /// <summary>
  /// A specialisation of <see cref="RemoteWebDriverProxy"/> which also implements
  /// <see cref="ICanReceiveScenarioStatus"/> in order to receive information about the success/failure of
  /// each test scenario (as is possible via Sauce Connect).
  /// </summary>
  public class SauceConnectRemoteWebDriverProxy : RemoteWebDriverProxy, ICanReceiveScenarioStatus
  {
    readonly ISuccessAndFailureGateway successFailureGateway;

    /// <summary>
    /// Sends information to the web driver indicating that the current test scenario was a failure.
    /// </summary>
    public void MarkScenarioAsFailure() => successFailureGateway.SendFailure(ProxiedDriver);

    /// <summary>
    /// Sends information to the web driver indicating that the current test scenario was a success.
    /// </summary>
    public void MarkScenarioAsSuccess() => successFailureGateway.SendSuccess(ProxiedDriver);

    /// <summary>
    /// Initializes a new instance of the <see cref="SauceConnectRemoteWebDriverProxy"/> class.
    /// </summary>
    /// <param name="proxiedDriver">The proxied/wrapped web driver.</param>
    /// <param name="flags">A collection of the browser flags for the driver.</param>
    /// <param name="successFailureGateway">Success and failure gateway service.</param>
    /// <param name="requestedVersion">The originally-requested browser version.</param>
    public SauceConnectRemoteWebDriverProxy(RemoteWebDriver proxiedDriver,
                                            ICollection<string> flags = null,
                                            ISuccessAndFailureGateway successFailureGateway = null,
                                            string requestedVersion = null)
      : base(proxiedDriver, flags, requestedVersion)
    {
      this.successFailureGateway = successFailureGateway ?? new SauceLabsSuccessFailureGateway();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SauceConnectRemoteWebDriverProxy"/> class.
    /// </summary>
    /// <param name="proxiedDriver">The proxied/wrapped web driver.</param>
    /// <param name="flags">A collection of the browser flags for the driver.</param>
    /// <param name="successFailureGateway">Success and failure gateway service.</param>
    /// <param name="requestedVersion">The originally-requested browser version.</param>
    public SauceConnectRemoteWebDriverProxy(RemoteWebDriver proxiedDriver,
                                            IReadOnlyCollection<string> flags,
                                            ISuccessAndFailureGateway successFailureGateway = null,
                                            string requestedVersion = null)
      : base(proxiedDriver, flags, requestedVersion)
    {
      this.successFailureGateway = successFailureGateway ?? new SauceLabsSuccessFailureGateway();
    }
  }
}

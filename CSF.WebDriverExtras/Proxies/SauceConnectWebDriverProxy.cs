using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Proxies
{
  public class SauceConnectWebDriverProxy : RemoteWebDriverProxy, ICanReceiveScenarioStatus
  {
    readonly ISuccessAndFailureGateway successFailureGateway;

    public void MarkScenarioAsFailure() => successFailureGateway.SendFailure(ProxiedDriver);

    public void MarkScenarioAsSuccess() => successFailureGateway.SendSuccess(ProxiedDriver);

    public SauceConnectWebDriverProxy(RemoteWebDriver proxiedDriver,
                                      ICollection<string> flags = null,
                                      ISuccessAndFailureGateway successFailureGateway = null)
      : base(proxiedDriver, flags)
    {
      this.successFailureGateway = successFailureGateway ?? new SauceLabsSuccessFailureGateway();
    }

    public SauceConnectWebDriverProxy(RemoteWebDriver proxiedDriver,
                                      IReadOnlyCollection<string> flags,
                                      ISuccessAndFailureGateway successFailureGateway = null)
      : base(proxiedDriver, flags)
    {
      this.successFailureGateway = successFailureGateway ?? new SauceLabsSuccessFailureGateway();
    }
  }
}

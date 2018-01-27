using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using CSF.WebDriverExtras.SuccessAndFailure;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras
{
  public class SauceConnectWebDriverProvider : WebDriverProvider, IReceivesScenarioName
  {
    //TestNameCapabilityName = "name",

    public void SetScenarioName(string name)
    {
      throw new NotImplementedException();
    }

    public SauceConnectWebDriverProvider(IWebDriver webDriver,
                                         string browserName,
                                         string browserVersion,
                                         string platform,
                                         IDictionary<string, object> requestedCapabilities,
                                         object options,
                                         IGetsBrowserFlags flagsProvider = null,
                                         ISuccessAndFailureGateway successFailureGateway = null)
      : base(webDriver,
             browserName,
             browserVersion,
             platform,
             requestedCapabilities,
             options,
             flagsProvider,
             successFailureGateway) {}
  }
}

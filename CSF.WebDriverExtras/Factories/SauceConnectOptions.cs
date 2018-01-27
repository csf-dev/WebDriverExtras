using System;
namespace CSF.WebDriverExtras.Factories
{
  public class SauceConnectOptions : RemoteOptions
  {
    public string TunnelIdentifier { get; set; }

    public string SauceConnectUsername { get; set; }

    public string SauceConnectApiKey { get; set; }

    public string SauceLabsBuildName { get; set; }
  }
}

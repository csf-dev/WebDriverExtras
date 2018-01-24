using System;
namespace CSF.WebDriverExtras.Factories
{
  public class RemoteOptions
  {
    static internal readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

    static internal readonly Uri
      DefaultRemoteWebDriverEndpoint = new Uri("http://127.0.0.1:4444/wd/hub");

    /// <summary>
    /// Gets or sets the desired browser name.
    /// </summary>
    /// <value>The name of the browser.</value>
    public string BrowserName { get; set; }

    /// <summary>
    /// Gets or sets the desired browser version.
    /// </summary>
    /// <value>The browser version.</value>
    public string BrowserVersion { get; set; }

    /// <summary>
    /// Gets or sets the desired platform.
    /// </summary>
    /// <value>The platform.</value>
    public string Platform { get; set; }

    /// <summary>
    /// Gets or sets the endpoint address for the remote web browser driver.
    /// </summary>
    /// <value>The remote address.</value>
    public string RemoteWebDriverAddress { get; set; }

    /// <summary>
    /// Gets or sets the timeout (in seconds) between issuing a command to the web driver and receiving a response.
    /// </summary>
    /// <value>The command timeout seconds.</value>
    public int? CommandTimeoutSeconds { get; set; }

    public TimeSpan? GetCommandTimeout()
    {
      if(CommandTimeoutSeconds == null)
        return null;

      return TimeSpan.FromSeconds(CommandTimeoutSeconds.Value);
    }

    public Uri GetRemoteWebDriverEndpoint()
    {
      if(String.IsNullOrEmpty(RemoteWebDriverAddress))
        return DefaultRemoteWebDriverEndpoint;

      return new Uri(RemoteWebDriverAddress);
    }
  }
}

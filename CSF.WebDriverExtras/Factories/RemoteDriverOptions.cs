using System;
namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Web driver factory options which relate to 'remote' web drivers (accessed via a remote
  /// API call)
  /// </summary>
  public class RemoteDriverOptions
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
    /// Gets or sets the timeout between issuing the web driver a command and receiving a response (in seconds).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Leave this blank to use the default timeout of 60 seconds.
    /// </para>
    /// </remarks>
    /// <value>The command timeout.</value>
    public int? CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets the command timeout as a <c>System.TimeSpan</c> instance.  This is controlled by
    /// <see cref="CommandTimeoutSeconds"/>.
    /// </summary>
    /// <returns>The command timeout.</returns>
    public TimeSpan? GetCommandTimeout()
    {
      if(CommandTimeoutSeconds == null)
        return null;

      return TimeSpan.FromSeconds(CommandTimeoutSeconds.Value);
    }

    /// <summary>
    /// Gets the endpoint of the API which provides the web driver.
    /// </summary>
    /// <returns>The web driver API endpoint.</returns>
    public Uri GetRemoteWebDriverEndpoint()
    {
      if(String.IsNullOrEmpty(RemoteWebDriverAddress))
        return DefaultRemoteWebDriverEndpoint;

      return new Uri(RemoteWebDriverAddress);
    }
  }
}

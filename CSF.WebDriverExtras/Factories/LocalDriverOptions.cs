using System;
namespace CSF.WebDriverExtras.Factories
{
  /// <summary>
  /// Base class for web driver factory options which relate to 'local' web drivers (running on the same
  /// computer as the code which consumes the web driver)
  /// </summary>
  public abstract class LocalDriverOptions
  {
    static internal readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(15);

    /// <summary>
    /// Gets or sets the timeout between issuing the web driver a command and receiving a response (in seconds).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Leave this blank to use the default timeout of 15 seconds.
    /// </para>
    /// </remarks>
    /// <value>The command timeout.</value>
    public int? CommandTimeoutSeconds { get; set; }

    /// <summary>
    /// Gets or sets the TCP port number for the web driver.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Leave this blank to use the default port, selected by the web driver itself.
    /// </para>
    /// </remarks>
    /// <value>The web driver port.</value>
    public int? WebDriverPort { get; set; }

    /// <summary>
    /// Gets or sets the file system path to the web driver executable.
    /// </summary>
    /// <value>The web driver executable path.</value>
    public string WebDriverExecutablePath { get; set; }

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
  }
}

using System;
namespace CSF.WebDriverExtras.Factories
{
  public abstract class LocalDriverOptions
  {
    static internal readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

    public int? CommandTimeoutSeconds { get; set; }

    public int? WebDriverPort { get; set; }

    public string WebDriverExecutablePath { get; set; }

    public string BrowserVersion { get; set; }

    public TimeSpan? GetCommandTimeout()
    {
      if(CommandTimeoutSeconds == null)
        return null;

      return TimeSpan.FromSeconds(CommandTimeoutSeconds.Value);
    }
  }
}

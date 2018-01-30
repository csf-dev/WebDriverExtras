using System;
namespace CSF.WebDriverExtras.Flags
{
  public class BrowserIdentification
  {
    public string Name { get; private set; }

    public string Platform { get; private set; }

    public IBrowserVersion Version { get; private set; }

    public BrowserIdentification(string name, IBrowserVersion version, string platform)
    {
      if(name == null)
        throw new ArgumentNullException(nameof(name));
      if(version == null)
        throw new ArgumentNullException(nameof(version));
      if(platform == null)
        throw new ArgumentNullException(nameof(platform));

      Name = name;
      Version = version;
      Platform = platform;
    }
  }
}

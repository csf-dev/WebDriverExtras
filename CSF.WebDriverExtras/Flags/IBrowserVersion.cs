using System;
namespace CSF.WebDriverExtras.Flags
{
  public interface IBrowserVersion : IComparable<IBrowserVersion>
  {
    string ToString();
  }
}

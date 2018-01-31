using System;
namespace CSF.WebDriverExtras.Flags
{
  public abstract class BrowserVersion : IComparable<BrowserVersion>, IEquatable<BrowserVersion>
  {
    public abstract int CompareTo(BrowserVersion other);

    public abstract bool Equals(BrowserVersion other);

    public override bool Equals(object obj) => Equals(obj as BrowserVersion);

    public override int GetHashCode() => base.GetHashCode();

    public static bool operator < (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(first, null))
        return true;

      return first.CompareTo(second) < 0;
    }

    public static bool operator > (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(second, null))
        return true;

      return first.CompareTo(second) > 0;
    }

    public static bool operator <= (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(first, null))
        return true;

      return first.CompareTo(second) <= 0;
    }

    public static bool operator >= (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(second, null))
        return true;

      return first.CompareTo(second) >= 0;
    }

    public static bool operator == (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second)) return true;
      if(ReferenceEquals(first, null)) return false;
      return first.Equals(second);
    }

    public static bool operator != (BrowserVersion first, BrowserVersion second) => !(first == second);
  }
}

using System;
namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Describes the version of a web browser.
  /// </summary>
  public abstract class BrowserVersion : IComparable<BrowserVersion>, IEquatable<BrowserVersion>
  {
    /// <summary>
    /// Compares the current instance to another <see cref="BrowserVersion"/> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public abstract int CompareTo(BrowserVersion other);

    /// <summary>
    /// Determines whether the specified <see cref="BrowserVersion"/> is equal to the current <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="BrowserVersion"/> to compare with the current <see cref="BrowserVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="BrowserVersion"/>; otherwise, <c>false</c>.</returns>
    public abstract bool Equals(BrowserVersion other);

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="BrowserVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
    /// <see cref="BrowserVersion"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj) => Equals(obj as BrowserVersion);

    /// <summary>
    /// Serves as a hash function for a <see cref="BrowserVersion"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() => base.GetHashCode();

    /// <summary>
    /// Determines whether one specified <see cref="BrowserVersion"/> is lower than another
    /// specfied <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> is lower than <c>second</c>; otherwise, <c>false</c>.</returns>
    public static bool operator < (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(first, null))
        return true;

      return first.CompareTo(second) < 0;
    }

    /// <summary>
    /// Determines whether one specified <see cref="BrowserVersion"/> is greater than another
    /// specfied <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> is greater than <c>second</c>; otherwise, <c>false</c>.</returns>
    public static bool operator > (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(second, null))
        return true;

      return first.CompareTo(second) > 0;
    }

    /// <summary>
    /// Determines whether one specified <see cref="BrowserVersion"/> is lower than or equal
    /// to another specfied <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> is lower than or equal to <c>second</c>; otherwise, <c>false</c>.</returns>
    public static bool operator <= (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(first, null))
        return true;

      return first.CompareTo(second) <= 0;
    }

    /// <summary>
    /// Determines whether one specified <see cref="BrowserVersion"/> is greater than or equal
    /// to another specfied <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> is greater than or equal to <c>second</c>; otherwise, <c>false</c>.</returns>
    public static bool operator >= (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second))
        return false;
      if(ReferenceEquals(second, null))
        return true;

      return first.CompareTo(second) >= 0;
    }

    /// <summary>
    /// Determines whether a specified instance of <see cref="BrowserVersion"/> is equal to
    /// another specified <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> and <c>second</c> are equal; otherwise, <c>false</c>.</returns>
    public static bool operator == (BrowserVersion first, BrowserVersion second)
    {
      if(ReferenceEquals(first, second)) return true;
      if(ReferenceEquals(first, null)) return false;
      return first.Equals(second);
    }

    /// <summary>
    /// Determines whether a specified instance of <see cref="BrowserVersion"/> is not equal
    /// to another specified <see cref="BrowserVersion"/>.
    /// </summary>
    /// <param name="first">The first <see cref="BrowserVersion"/> to compare.</param>
    /// <param name="second">The second <see cref="BrowserVersion"/> to compare.</param>
    /// <returns><c>true</c> if <c>first</c> and <c>second</c> are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator != (BrowserVersion first, BrowserVersion second) => !(first == second);
  }
}

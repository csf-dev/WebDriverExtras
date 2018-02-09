using System;
namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// Represents an empty or missing browser version.  Used when the string browser version is empty.
  /// </summary>
  public class EmptyBrowserVersion : BrowserVersion, IComparable<EmptyBrowserVersion>, IEquatable<EmptyBrowserVersion>
  {
    static readonly EmptyBrowserVersion singleton;

    /// <summary>
    /// Compares the current instance to another <see cref="BrowserVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public override int CompareTo(BrowserVersion other) => CompareTo(other as EmptyBrowserVersion);

    /// <summary>
    /// Determines whether the specified <see cref="BrowserVersion"/> is equal to the current <see cref="EmptyBrowserVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="BrowserVersion"/> to compare with the current <see cref="EmptyBrowserVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="EmptyBrowserVersion"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(BrowserVersion other) => Equals(other as EmptyBrowserVersion);

    /// <summary>
    /// Compares the current instance to another <see cref="EmptyBrowserVersion" /> instance, returning eiher
    /// one or the other version is null, or zero if it is not.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public int CompareTo(EmptyBrowserVersion other)
    {
      if(other == null) return 1;
      return 0;
    }

    /// <summary>
    /// Determines whether the specified <see cref="EmptyBrowserVersion"/> is equal to the current <see cref="EmptyBrowserVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="EmptyBrowserVersion"/> to compare with the current <see cref="EmptyBrowserVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="EmptyBrowserVersion"/> is equal to the current
    /// <see cref="EmptyBrowserVersion"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(EmptyBrowserVersion other)
    {
      if(other == null) return false;
      return true;
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="EmptyBrowserVersion"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="EmptyBrowserVersion"/>.</returns>
    public override string ToString() => "[Unknown version]";

    EmptyBrowserVersion() {}

    /// <summary>
    /// Initializes the <see cref="T:CSF.WebDriverExtras.BrowserId.EmptyBrowserVersion"/> class.
    /// </summary>
    static EmptyBrowserVersion()
    {
      singleton = new EmptyBrowserVersion();
    }

    /// <summary>
    /// Gets a singleton instance for this class.
    /// </summary>
    /// <value>The singleton.</value>
    public static EmptyBrowserVersion Singleton => singleton;
  }
}

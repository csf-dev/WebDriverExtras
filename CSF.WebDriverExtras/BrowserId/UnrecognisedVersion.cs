using System;
namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// Implementation of <see cref="BrowserVersion"/> which works for any version string.
  /// It is a fall-back position, because it uses simple string comparison for equality and (worse) for greater-than
  /// and less-than comparison.
  /// </summary>
  public class UnrecognisedVersion : BrowserVersion, IComparable<UnrecognisedVersion>, IEquatable<UnrecognisedVersion>
  {
    readonly string versionString;

    /// <summary>
    /// Gets the version string.
    /// </summary>
    /// <value>The version string.</value>
    public string VersionString => versionString;

    /// <summary>
    /// Compares the current instance to another <see cref="BrowserVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public override int CompareTo(BrowserVersion other) => CompareTo(other as UnrecognisedVersion);

    /// <summary>
    /// Determines whether the specified <see cref="BrowserVersion"/> is equal to the current <see cref="UnrecognisedVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="BrowserVersion"/> to compare with the current <see cref="UnrecognisedVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="UnrecognisedVersion"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(BrowserVersion other) => Equals(other as UnrecognisedVersion);

    /// <summary>
    /// Compares the current instance to another <see cref="UnrecognisedVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public virtual int CompareTo(UnrecognisedVersion other)
    {
      if(ReferenceEquals(other, null))
        return -1;

      return StringComparer.InvariantCulture.Compare(VersionString, other.VersionString);
    }

    /// <summary>
    /// Determines whether the specified <see cref="UnrecognisedVersion"/> is equal to the current <see cref="UnrecognisedVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="UnrecognisedVersion"/> to compare with the current <see cref="UnrecognisedVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="UnrecognisedVersion"/>; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(UnrecognisedVersion other)
    {
      if(ReferenceEquals(other, null))
        return false;

      return StringComparer.InvariantCulture.Equals(VersionString, other.VersionString);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="UnrecognisedVersion"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      var str = VersionString ?? String.Empty;
      return str.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="UnrecognisedVersion"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="UnrecognisedVersion"/>.</returns>
    public override string ToString() => $"Unrecognised version:{VersionString ?? "<null>"}";

    /// <summary>
    /// Initializes a new instance of the <see cref="UnrecognisedVersion"/> class.
    /// </summary>
    /// <param name="versionString">Version string.</param>
    public UnrecognisedVersion(string versionString)
    {
      this.versionString = versionString;
    }
  }
}

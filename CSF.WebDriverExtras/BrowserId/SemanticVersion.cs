using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// Implementation of <see cref="BrowserVersion"/> which follows the semantic versioning v2.0.0 spec
  /// </summary>
  /// <remarks>
  /// <para>
  /// The semantic versioning specification may be found at https://semver.org/spec/v2.0.0.html
  /// </para>
  /// </remarks>
  public class SemanticVersion : BrowserVersion, IComparable<SemanticVersion>, IEquatable<SemanticVersion>
  {
    const string
      NumericPattern = @"^\d+$",
      ParserPattern = @"^v(\d+)\.(\d+)\.(\d+)(?:-(?:([0-9A-Za-z-]+)\.?)+)?(?:\+(?:([0-9A-Za-z-]+)\.?)+)?$";
    static readonly Regex
      Numeric = new Regex(NumericPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant),
      Parser = new Regex(ParserPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

    readonly int major, minor, patch;
    readonly IReadOnlyList<string> prereleaseIdentifiers;
    readonly IReadOnlyList<string> metadata;

    /// <summary>
    /// Gets the major version component.
    /// </summary>
    /// <value>The major version component.</value>
    public int Major => major;

    /// <summary>
    /// Gets the minor version component.
    /// </summary>
    /// <value>The minor version component.</value>
    public int Minor => minor;

    /// <summary>
    /// Gets the patch version component.
    /// </summary>
    /// <value>The patch version component.</value>
    public int Patch => patch;

    /// <summary>
    /// Gets an ordered collection of the pre-release identifiers which apply to this version.
    /// </summary>
    /// <value>The prerelease identifiers.</value>
    public IReadOnlyList<string> PrereleaseIdentifiers => prereleaseIdentifiers;

    /// <summary>
    /// Gets an ordered collection of release metadata which is present in this version.
    /// </summary>
    /// <value>The metadata.</value>
    public IReadOnlyList<string> Metadata => metadata;

    /// <summary>
    /// Compares the current instance to another <see cref="BrowserVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public override int CompareTo(BrowserVersion other) => CompareTo(other as SemanticVersion);

    /// <summary>
    /// Compares the current instance to another <see cref="SemanticVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public int CompareTo(SemanticVersion other)
    {
      if(other == null) return 1;

      var versionComparison = CompareToBasedUponMajorMinorPatchVersions(other);
      if(versionComparison.HasValue)
        return versionComparison.Value;

      var standardOrPrereleaseComparison = CompareToBasedUponStandardOrPrereleaseStatus(other);
      if(standardOrPrereleaseComparison.HasValue)
        return standardOrPrereleaseComparison.Value;

      return CompareToBasedOnPrereleaseIdentifiers(other);
    }

    int? CompareToBasedUponMajorMinorPatchVersions(SemanticVersion other)
    {
      if(Major != other.Major)
        return Major.CompareTo(other.Major);
      if(Minor != other.Minor)
        return Minor.CompareTo(other.Minor);
      if(Patch != other.Patch)
        return Patch.CompareTo(other.Patch);

      return null;
    }

    int? CompareToBasedUponStandardOrPrereleaseStatus(SemanticVersion other)
    {
      if(!PrereleaseIdentifiers.Any() && !other.PrereleaseIdentifiers.Any())
        return 0;
      if(!PrereleaseIdentifiers.Any() && other.PrereleaseIdentifiers.Any())
        return 1;
      if(PrereleaseIdentifiers.Any() && !other.PrereleaseIdentifiers.Any())
        return -1;

      return null;
    }

    int CompareToBasedOnPrereleaseIdentifiers(SemanticVersion other)
    {
      for(var i = 0; i < PrereleaseIdentifiers.Count; i++)
      {
        // I have more identifiers than the other and all common identifiers are equal
        if(i >= other.PrereleaseIdentifiers.Count)
          return 1;

        var myIdentifier = PrereleaseIdentifiers[i];
        var theirIdentifier = other.PrereleaseIdentifiers[i];

        var identifierComparison = ComparePrereleaseIdentifiers(myIdentifier, theirIdentifier);
        if(identifierComparison.HasValue)
          return identifierComparison.Value;
      }

      // All common identifiers are equal but the other has more than me
      if(other.PrereleaseIdentifiers.Count > PrereleaseIdentifiers.Count)
        return -1;

      // We're exactly equal
      return 0;
    }

    int? ComparePrereleaseIdentifiers(string mine, string theirs)
    {
      var mineIsNumeric = IsNumeric(mine);
      var theirsIsNumeric = IsNumeric(theirs);

      if(mine == theirs)
        return null;
      if(mineIsNumeric && !theirsIsNumeric)
        return -1;
      if(!mineIsNumeric && theirsIsNumeric)
        return 1;
      if(!mineIsNumeric && !theirsIsNumeric)
        return StringComparer.InvariantCulture.Compare(mine, theirs);
        
      var numericMine = Int32.Parse(mine);
      var numericTheirs = Int32.Parse(theirs);

      if(numericMine == numericTheirs)
        return null;

      return numericMine.CompareTo(numericTheirs);
    }

    bool IsNumeric(string str) => Numeric.IsMatch(str);

    /// <summary>
    /// Determines whether the specified <see cref="BrowserVersion"/> is equal to the current <see cref="SemanticVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="BrowserVersion"/> to compare with the current <see cref="SemanticVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="SemanticVersion"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(BrowserVersion other) => Equals(other as SemanticVersion);

    /// <summary>
    /// Determines whether the specified <see cref="SemanticVersion"/> is equal to the current <see cref="SemanticVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="SemanticVersion"/> to compare with the current <see cref="SemanticVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="SemanticVersion"/> is equal to the current
    /// <see cref="SemanticVersion"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(SemanticVersion other)
    {
      if(ReferenceEquals(other, null))
        return false;

      return (Major == other.Major
              && Minor == other.Minor
              && Patch == other.Patch
              && PrereleaseIdentifiers.SequenceEqual(other.PrereleaseIdentifiers));
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="SemanticVersion"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode()
    {
      return (Major.GetHashCode()
              ^ Minor.GetHashCode()
              ^ Patch.GetHashCode()
              ^ PrereleaseIdentifiers.GetHashCode());
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="SemanticVersion"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="SemanticVersion"/>.</returns>
    public override string ToString()
    {
      var presumedPrefix = IsPresumedVersion? "[presumed] " : String.Empty;
      return $"{presumedPrefix}v{Major}.{Minor}.{Patch}{GetPrereleaseIdentifiersString()}{GetMetadataString()}";
    }

    string GetPrereleaseIdentifiersString()
    {
      if(!PrereleaseIdentifiers.Any())
        return String.Empty;

      return "-" + String.Join(".", PrereleaseIdentifiers);
    }

    string GetMetadataString()
    {
      if(!Metadata.Any())
        return String.Empty;

      return "+" + String.Join(".", Metadata);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SemanticVersion"/> class.
    /// </summary>
    /// <param name="major">Major.</param>
    /// <param name="minor">Minor.</param>
    /// <param name="patch">Patch.</param>
    /// <param name="prereleaseIdentifiers">Prerelease identifiers.</param>
    /// <param name="metadata">Metadata.</param>
    /// <param name="isPresumed">If set to <c>true</c> then this instance will represent a presumed version number.</param>
    public SemanticVersion(int major, int minor, int patch,
                           IList<string> prereleaseIdentifiers = null,
                           IList<string> metadata = null,
                           bool isPresumed = false) : base(isPresumed)
    {
      if(major < 0) throw new ArgumentOutOfRangeException(nameof(major), "No version number component may be negative");
      if(minor < 0) throw new ArgumentOutOfRangeException(nameof(minor), "No version number component may be negative");
      if(patch < 0) throw new ArgumentOutOfRangeException(nameof(patch), "No version number component may be negative");

      this.major = major;
      this.minor = minor;
      this.patch = patch;

      this.prereleaseIdentifiers = prereleaseIdentifiers?.ToArray() ?? new string[0];
      this.metadata = metadata?.ToArray() ?? new string[0];
    }

    /// <summary>
    /// Parses an input string which may or may not represent a properly-formed semantic version string.
    /// Returns either a semantic version instance or <c>null</c> if the string is not a properly-formed semantic version.
    /// </summary>
    /// <param name="versionString">Version string.</param>
    /// <param name="isPresumed">If set to <c>true</c> then this instance will represent a presumed version number.</param>
    public static SemanticVersion Parse(string versionString, bool isPresumed = false)
    {
      if(versionString == null) return null;

      var match = Parser.Match(versionString);
      if(!match.Success) return null;

      var major = Int32.Parse(match.Groups[1].Value);
      var minor = Int32.Parse(match.Groups[2].Value);
      var patch = Int32.Parse(match.Groups[3].Value);

      var prereleases = match.Groups[4].Captures.Cast<Capture>().Select(x => x.Value).ToList();
      var metadatas = match.Groups[5].Captures.Cast<Capture>().Select(x => x.Value).ToList();

      return new SemanticVersion(major, minor, patch, prereleases, metadatas, isPresumed);
    }
  }
}

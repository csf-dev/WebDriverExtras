using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// This follows Semantic versioning v2.0.0: https://semver.org/spec/v2.0.0.html
  /// </summary>
  public class SemanticVersion : IBrowserVersion, IComparable<SemanticVersion>
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

    public int Major => major;

    public int Minor => minor;

    public int Patch => patch;

    public IReadOnlyList<string> PrereleaseIdentifiers => prereleaseIdentifiers;

    public IReadOnlyList<string> Metadata => metadata;

    public int CompareTo(IBrowserVersion other) => CompareTo(other as SemanticVersion);

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

    public override string ToString()
      => $"v{Major}.{Minor}.{Patch}{GetPrereleaseIdentifiersString()}{GetMetadataString()}";

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

    public SemanticVersion(int major, int minor, int patch,
                           IList<string> prereleaseIdentifiers = null,
                           IList<string> metadata = null)
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

    public static SemanticVersion Parse(string versionString)
    {
      if(versionString == null) return null;

      var match = Parser.Match(versionString);
      if(!match.Success) return null;

      var major = Int32.Parse(match.Groups[1].Value);
      var minor = Int32.Parse(match.Groups[2].Value);
      var patch = Int32.Parse(match.Groups[3].Value);

      var prereleases = match.Groups[4].Captures.Cast<Capture>().Select(x => x.Value).ToList();
      var metadatas = match.Groups[5].Captures.Cast<Capture>().Select(x => x.Value).ToList();

      return new SemanticVersion(major, minor, patch, prereleases, metadatas);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSF.WebDriverExtras.BrowserId
{
  public class DottedNumericVersion : BrowserVersion, IComparable<DottedNumericVersion>, IEquatable<DottedNumericVersion>
  {
    const string
      ParserPattern = @"^(\d+)(?:\.(\d+))*$";

    static readonly Regex
      Parser = new Regex(ParserPattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

    readonly IReadOnlyList<int> components;

    public IReadOnlyList<int> Components => components;

    /// <summary>
    /// Compares the current instance to another <see cref="BrowserVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public override int CompareTo(BrowserVersion other) => CompareTo(other as DottedNumericVersion);

    /// <summary>
    /// Compares the current instance to another <see cref="DottedNumericVersion" /> instance, returning eiher
    /// minus one, zero or one.
    /// </summary>
    /// <param name="other">The browser version to compare with.</param>
    public int CompareTo(DottedNumericVersion other)
    {
      if(other == null) return 1;

      var currentComponentIndex = 0;
      while(currentComponentIndex < Components.Count)
      {
        // If all components so far have been equal and this version has more components
        // than the other, then this version is higher than the other.
        if(currentComponentIndex >= other.Components.Count)
          return 1;

        int
          thisComponent = Components[currentComponentIndex],
          otherComponent = other.Components[currentComponentIndex],
          componentDifference = thisComponent - otherComponent;

        // If the current component differs then return a comparison based upon that.
        if(componentDifference < 0) return -1;
        if(componentDifference > 0) return 1;
        
        // The current component is the same for both versions, move to the next
        currentComponentIndex ++;
      }

      // At this stage, all components which are shared by both versions are equal.
      // If the other version has more components than this one then the other version is higher.
      if(other.Components.Count > Components.Count) return -1;

      // The versions must be equal
      return 0;
    }

    /// <summary>
    /// Determines whether the specified <see cref="BrowserVersion"/> is equal to the current <see cref="DottedNumericVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="BrowserVersion"/> to compare with the current <see cref="DottedNumericVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="BrowserVersion"/> is equal to the current
    /// <see cref="DottedNumericVersion"/>; otherwise, <c>false</c>.</returns>
    public override bool Equals(BrowserVersion other) => Equals(other as DottedNumericVersion);

    /// <summary>
    /// Determines whether the specified <see cref="DottedNumericVersion"/> is equal to the current <see cref="DottedNumericVersion"/>.
    /// </summary>
    /// <param name="other">The <see cref="DottedNumericVersion"/> to compare with the current <see cref="DottedNumericVersion"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="DottedNumericVersion"/> is equal to the current
    /// <see cref="DottedNumericVersion"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(DottedNumericVersion other)
    {
      if(ReferenceEquals(other, null))
        return false;

      return Components.SequenceEqual(other.Components);
    }

    /// <summary>
    /// Serves as a hash function for a <see cref="DottedNumericVersion"/> object.
    /// </summary>
    /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
    public override int GetHashCode() => Components.Aggregate(0, (acc, next) => acc ^ next);

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="DottedNumericVersion"/>.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="DottedNumericVersion"/>.</returns>
    public override string ToString() => String.Join(".", Components);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.BrowserId.DottedNumericVersion"/> class.
    /// </summary>
    /// <param name="components">Components.</param>
    public DottedNumericVersion(IList<int> components)
    {
      if(components == null)
        throw new ArgumentNullException(nameof(components));

      this.components = components.ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:CSF.WebDriverExtras.BrowserId.DottedNumericVersion"/> class.
    /// </summary>
    /// <param name="components">Components.</param>
    public DottedNumericVersion(params int[] components)
    {
      if(components == null)
        throw new ArgumentNullException(nameof(components));

      this.components = components.ToArray();
    }

    /// <summary>
    /// Parses an input string which may or may not represent a dotted-numeric version string.
    /// Returns either a version instance or <c>null</c> if the string is not a properly-formed.
    /// </summary>
    /// <param name="versionString">Version string.</param>
    public static DottedNumericVersion Parse(string versionString)
    {
      if(versionString == null) return null;

      var match = Parser.Match(versionString);
      if(!match.Success) return null;

      var firstComponent = Int32.Parse(match.Groups[1].Value);
      var components = match.Groups[2].Captures
        .Cast<Capture>()
        .Select(x => Int32.Parse(x.Value))
        .ToList();
      
      components.Insert(0, firstComponent);
      return new DottedNumericVersion(components);
    }
  }
}

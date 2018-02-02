using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSF.WebDriverExtras.Flags.Reading;
using Newtonsoft.Json;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// Implementation of <see cref="IReadsFlagsDefinitions"/> which reads JSON data and deserialises flags definitions
  /// from it.
  /// </summary>
  public class DefinitionReader : IReadsFlagsDefinitions
  {
    static readonly JsonSerializer converter;

    readonly ICreatesBrowserVersions versionFactory;

    /// <summary>
    /// Gets the flags definitions from a string (UTF-8 encoding is assumed).
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputString">A string.</param>
    public IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString)
      => GetFlagsDefinitions(inputString, Encoding.UTF8);

    /// <summary>
    /// Gets the flags definitions from a string.
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputString">A string.</param>
    /// <param name="encoding">The string encoding.</param>
    public IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString, Encoding encoding)
    {
      if(inputString == null) return new FlagsDefinition[0];
      if(encoding == null)
        throw new ArgumentNullException(nameof(encoding));

      var buffer = encoding.GetBytes(inputString);
      using(var stream = new MemoryStream(buffer))
      {
        return GetFlagsDefinitions(stream);
      }
    }

    /// <summary>
    /// Gets the flags definitions from a <c>System.Stream</c>.
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputStream">A stream, such as a file stream or manifest resource.</param>
    public IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(Stream inputStream)
    {
      if(inputStream == null) return new FlagsDefinition[0];
      
      using(var reader = new StreamReader(inputStream))
      {
        var deseralizedDefinitions = converter.Deserialize(reader, typeof(List<SerializableFlagsDefinition>));
        var typedDefinitions = (IList<SerializableFlagsDefinition>) deseralizedDefinitions;
        return MapDefinitions(typedDefinitions);
      }
    }

    IReadOnlyCollection<FlagsDefinition> MapDefinitions(IList<SerializableFlagsDefinition> definitions)
    {
      if(definitions == null) return new FlagsDefinition[0];

      return definitions.Select(MapDefinition).Where(x => x != null).ToArray();
    }

    FlagsDefinition MapDefinition(SerializableFlagsDefinition definition)
    {
      if(definition == null) return null;

      if(definition.BrowserName == null
         || !definition.BrowserName.Any())
        return null;

      if((definition.Flags == null || !definition.Flags.Any())
         && (definition.RemoveFlags == null || !definition.RemoveFlags.Any()))
         return null;

      var output = new FlagsDefinition {
        BrowserNames = new HashSet<string>(definition.BrowserName ?? Enumerable.Empty<string>()),
        AddFlags = new HashSet<string>(definition.Flags ?? Enumerable.Empty<string>()),
        RemoveFlags = new HashSet<string>(definition.RemoveFlags ?? Enumerable.Empty<string>()),
      };

      if(definition.Platform != null)
        output.Platforms = new HashSet<string>(definition.Platform ?? Enumerable.Empty<string>());

      if(definition.MinVersion != null)
        output.MinimumVersion = versionFactory.CreateVersion(definition.MinVersion);

      if(definition.MaxVersion != null)
        output.MaximumVersion = versionFactory.CreateVersion(definition.MaxVersion);

      return output;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DefinitionReader"/> class.
    /// </summary>
    public DefinitionReader() : this(null) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="DefinitionReader"/> class.
    /// </summary>
    /// <param name="versionFactory">A factory which creates version numbers.</param>
    public DefinitionReader(ICreatesBrowserVersions versionFactory)
    {
      this.versionFactory = versionFactory ?? new VersionFactory();
    }

    static DefinitionReader()
    {
      converter = JsonSerializer.CreateDefault();
    }
  }
}

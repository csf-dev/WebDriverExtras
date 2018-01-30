﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSF.WebDriverExtras.Flags.Reading;
using Newtonsoft.Json;

namespace CSF.WebDriverExtras.Flags
{
  public class DefinitionReader : IReadsFlagsDefinitions
  {
    static readonly JsonSerializer converter;

    public IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString)
      => GetFlagsDefinitions(inputString, Encoding.UTF8);

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
         || !definition.BrowserName.Any()
         || definition.Flags == null
         || !definition.Flags.Any())
        return null;

      var output = new FlagsDefinition {
        BrowserNames = new HashSet<string>(definition.BrowserName),
        Flags = new HashSet<string>(definition.Flags),
      };

      if(definition.Platform != null)
        output.Platforms = new HashSet<string>(definition.Platform);

      if(definition.MinVersion != null)
        output.MinimumVersion = SemanticVersion.Parse(definition.MinVersion);

      if(definition.MaxVersion != null)
        output.MaximumVersion = SemanticVersion.Parse(definition.MaxVersion);

      return output;
    }

    static DefinitionReader()
    {
      converter = JsonSerializer.CreateDefault();
    }
  }
}

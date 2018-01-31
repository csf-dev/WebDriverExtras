using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CSF.WebDriverExtras.Flags.Reading
{
  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
  class SerializableFlagsDefinition
  {
    [JsonProperty]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    internal List<string> BrowserName { get; set; }

    [JsonProperty]
    internal string MinVersion { get; set; }

    [JsonProperty]
    internal string MaxVersion { get; set; }

    [JsonProperty]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    internal List<string> Platform { get; set; }

    [JsonProperty]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    internal List<string> Flags { get; set; }

    [JsonProperty]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    internal List<string> RemoveFlags { get; set; }
  }
}

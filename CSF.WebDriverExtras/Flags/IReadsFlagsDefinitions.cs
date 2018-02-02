using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSF.WebDriverExtras.Flags
{
  /// <summary>
  /// A service which gets flags definitions from 'storage' data (strings or streams) and returns them as
  /// an object model.
  /// </summary>
  public interface IReadsFlagsDefinitions
  {
    /// <summary>
    /// Gets the flags definitions from a <c>System.Stream</c>.
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputStream">A stream, such as a file stream or manifest resource.</param>
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(Stream inputStream);

    /// <summary>
    /// Gets the flags definitions from a string (UTF-8 encoding is assumed).
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputString">A string.</param>
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString);

    /// <summary>
    /// Gets the flags definitions from a string.
    /// </summary>
    /// <returns>The flags definitions.</returns>
    /// <param name="inputString">A string.</param>
    /// <param name="encoding">The string encoding.</param>
    IReadOnlyCollection<FlagsDefinition> GetFlagsDefinitions(string inputString, Encoding encoding);
  }
}

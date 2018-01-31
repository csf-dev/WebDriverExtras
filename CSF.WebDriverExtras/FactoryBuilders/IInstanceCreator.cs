using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// A factory service which spawns instances of named types.
  /// </summary>
  public interface IInstanceCreator
  {
    /// <summary>
    /// Creates an instance of the given type.
    /// </summary>
    /// <returns>The created object instance.</returns>
    /// <param name="type">Type.</param>
    object CreateInstance(Type type);
  }
}

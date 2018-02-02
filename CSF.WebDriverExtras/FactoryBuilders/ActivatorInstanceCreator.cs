using System;
namespace CSF.WebDriverExtras.FactoryBuilders
{
  /// <summary>
  /// Implementation of <see cref="IInstanceCreator"/> which creates instances of types.
  /// </summary>
  public class ActivatorInstanceCreator : IInstanceCreator
  {
    /// <summary>
    /// Creates an instance of the given type.
    /// </summary>
    /// <returns>The created object instance.</returns>
    /// <param name="type">Type.</param>
    public object CreateInstance(Type type) => Activator.CreateInstance(type);
  }
}

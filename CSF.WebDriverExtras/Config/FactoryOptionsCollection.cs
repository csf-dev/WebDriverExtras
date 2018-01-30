using System;
using System.Configuration;

namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// Represents a collection of <see cref="FactoryOption"/> within a configuration file.
  /// </summary>
  public class FactoryOptionsCollection : ConfigurationElementCollection
  {
    internal const string PropertyName = "Property";

    /// <summary>
    /// Gets the configuration collection type.
    /// </summary>
    /// <value>The type of the collection.</value>
    public override ConfigurationElementCollectionType CollectionType
      => ConfigurationElementCollectionType.BasicMapAlternate;

    /// <summary>
    /// Gets the name of the element.
    /// </summary>
    /// <value>The name of the element.</value>
    protected override string ElementName => PropertyName;

    /// <summary>
    /// Determines whether the given string matches the <see cref="ElementName"/>.
    /// </summary>
    /// <returns><c>true</c> if the string matches the element name; otherwise, <c>false</c>.</returns>
    /// <param name="elementName">Element name.</param>
    protected override bool IsElementName(string elementName)
      => elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);

    /// <summary>
    /// Determines whether this instance is read only.
    /// </summary>
    /// <returns><c>true</c> if this instance is read only; otherwise, <c>false</c>.</returns>
    public override bool IsReadOnly() => false;

    /// <summary>
    /// Creates the new element.
    /// </summary>
    /// <returns>The new element.</returns>
    protected override ConfigurationElement CreateNewElement() => new FactoryOption();

    /// <summary>
    /// Gets the element key.
    /// </summary>
    /// <returns>The element key.</returns>
    /// <param name="element">Element.</param>
    protected override object GetElementKey(ConfigurationElement element) => ((FactoryOption) (element)).Name;

    /// <summary>
    /// Gets the <see cref="FactoryOption"/> with the specified index.
    /// </summary>
    /// <param name="idx">Index.</param>
    public FactoryOption this[int idx] => (FactoryOption) BaseGet(idx);
  }

  /// <summary>
  /// A single factory property, exposing a name/value pair, indicating a public settable property on the factory
  /// type and the value to be set into it.
  /// </summary>
  public class FactoryOption : ConfigurationElement
  {
    const string NameConfigName = @"Name", ValueConfigName = @"Value";

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty(NameConfigName, IsRequired = true)]
    public virtual string Name
    {
      get { return (string) this[NameConfigName]; }
      set { this[NameConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty(ValueConfigName, IsRequired = true)]
    public virtual string Value
    {
      get { return (string) this[ValueConfigName]; }
      set { this[ValueConfigName] = value; }
    }
  }
}

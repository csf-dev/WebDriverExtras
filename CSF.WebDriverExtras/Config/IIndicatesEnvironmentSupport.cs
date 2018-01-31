using System;
namespace CSF.WebDriverExtras.Config
{
  /// <summary>
  /// Extension of <see cref="IDescribesWebDriverFactory"/> which also indicates whether or not the
  /// configuration options for the web driver factory should be read from the environment variables.
  /// </summary>
  public interface IIndicatesEnvironmentSupport : IDescribesWebDriverFactory
  {
    /// <summary>
    /// Gets or sets a value indicating whether environment variables should be read when creating
    /// the webdriver factory options.
    /// </summary>
    /// <value><c>true</c> if environment variable support enabled; otherwise, <c>false</c>.</value>
    bool EnvironmentVariableSupportEnabled { get; }

    /// <summary>
    /// Gets the prefix for environment variables which control the webdriver factory options.
    /// </summary>
    string GetEnvironmentVariablePrefix();
  }
}

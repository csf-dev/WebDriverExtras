using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Config;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CSF.WebDriverExtras.Tests.Config
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class EnvironmentVariableFactoryConfigReaderProxyTests
  {
    [Test,AutoMoqData]
    public void GetFactoryAssemblyQualifiedTypeName_returns_result_from_proxied_type(IDescribesWebDriverFactory proxied,
                                                                                     string baseResult)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied);
      Mock.Get(proxied).Setup(x => x.GetFactoryAssemblyQualifiedTypeName()).Returns(baseResult);

      // Act
      var result = sut.GetFactoryAssemblyQualifiedTypeName();

      // Assert
      Assert.That(result, Is.EqualTo(baseResult));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_returns_same_result_as_proxied_when_environment_cannot_be_supported(IDescribesWebDriverFactory proxied,
                                                                                                       [HasValues] Dictionary<string,string> proxiedResult)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied);
      Mock.Get(proxied).Setup(x => x.GetOptionKeyValuePairs()).Returns(proxiedResult);

      // Act
      var result = sut.GetOptionKeyValuePairs();

      // Assert
      Assert.That(result, Is.EquivalentTo(proxiedResult));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_returns_same_result_as_proxied_when_environment_is_not_supported(IIndicatesEnvironmentSupport proxied,
                                                                                                    [HasValues] Dictionary<string,string> proxiedResult,
                                                                                                    IReadsEnvironmentVariables environment)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied, environment);
      Mock.Get(proxied).Setup(x => x.GetOptionKeyValuePairs()).Returns(proxiedResult);
      Mock.Get(proxied).SetupGet(x => x.EnvironmentVariableSupportEnabled).Returns(false);
      Mock.Get(proxied).Setup(x => x.GetEnvironmentVariablePrefix()).Returns("E_");
      Mock.Get(environment)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(new Dictionary<string,string> { { "E_EnvironmentVar", "EnvironmentValue" } });

      // Act
      var result = sut.GetOptionKeyValuePairs();

      // Assert
      Assert.That(result, Is.EquivalentTo(proxiedResult));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_adds_environment_values_when_it_is_supported(IIndicatesEnvironmentSupport proxied,
                                                                                [HasValues] Dictionary<string,string> proxiedResult,
                                                                                IReadsEnvironmentVariables environment)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied, environment);
      Mock.Get(proxied).Setup(x => x.GetOptionKeyValuePairs()).Returns(proxiedResult);
      Mock.Get(proxied).SetupGet(x => x.EnvironmentVariableSupportEnabled).Returns(true);
      Mock.Get(proxied).Setup(x => x.GetEnvironmentVariablePrefix()).Returns("E_");
      Mock.Get(environment)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(new Dictionary<string,string> { { "E_EnvironmentVar", "EnvironmentValue" } });
      var expected = new Dictionary<string,string>(proxiedResult);
      expected.Add("EnvironmentVar", "EnvironmentValue");

      // Act
      var result = sut.GetOptionKeyValuePairs();

      // Assert
      Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_ignores_environment_variables_which_do_not_start_with_the_right_prefix(IIndicatesEnvironmentSupport proxied,
                                                                                                          [HasValues] Dictionary<string,string> proxiedResult,
                                                                                                          IReadsEnvironmentVariables environment)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied, environment);
      Mock.Get(proxied).Setup(x => x.GetOptionKeyValuePairs()).Returns(proxiedResult);
      Mock.Get(proxied).SetupGet(x => x.EnvironmentVariableSupportEnabled).Returns(true);
      Mock.Get(proxied).Setup(x => x.GetEnvironmentVariablePrefix()).Returns("E_");
      Mock.Get(environment)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(new Dictionary<string,string> { { "XX_EnvironmentVar", "EnvironmentValue" } });
      var expected = new Dictionary<string,string>(proxiedResult);

      // Act
      var result = sut.GetOptionKeyValuePairs();

      // Assert
      Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test,AutoMoqData]
    public void GetProviderOptions_gives_environment_values_precedence_over_base_results(IIndicatesEnvironmentSupport proxied,
                                                                                         [HasValues(0)] Dictionary<string,string> proxiedResult,
                                                                                         IReadsEnvironmentVariables environment)
    {
      // Arrange
      var sut = new EnvironmentVariableFactoryDescriptionProxy(proxied, environment);
      proxiedResult.Add("VariableName", "ValueFromBase");
      Mock.Get(proxied).Setup(x => x.GetOptionKeyValuePairs()).Returns(proxiedResult);
      Mock.Get(proxied).SetupGet(x => x.EnvironmentVariableSupportEnabled).Returns(true);
      Mock.Get(proxied).Setup(x => x.GetEnvironmentVariablePrefix()).Returns("E_");
      Mock.Get(environment)
          .Setup(x => x.GetEnvironmentVariables())
          .Returns(new Dictionary<string,string> { { "E_VariableName", "ValueFromEnvironment" } });
      var expected = new Dictionary<string,string>{ { "VariableName", "ValueFromEnvironment" } };

      // Act
      var result = sut.GetOptionKeyValuePairs();

      // Assert
      Assert.That(result, Is.EquivalentTo(expected));
    }
  }
}

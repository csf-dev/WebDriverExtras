using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Config;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;

namespace CSF.WebDriverExtras.Tests.Config
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class ConfigurationWebDriverProviderFactoryProxyTests
  {
    [Test,AutoMoqData]
    public void CreateProvider_calls_method_from_proxied_type([Frozen] ICreatesWebDriverProvidersWithOptions proxied,
                                                              ConfigurationWebDriverProviderFactoryProxy sut)
    {
      // Act
      sut.CreateProvider();

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateProvider(It.IsAny<object>(), null, null), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_returns_result_from_proxied_type([Frozen] ICreatesWebDriverProvidersWithOptions proxied,
                                                                ConfigurationWebDriverProviderFactoryProxy sut,
                                                                IProvidesWebDriver provider)
    {
      // Arrange
      Mock.Get(proxied)
          .Setup(x => x.CreateProvider(It.IsAny<object>(), null, null))
          .Returns(provider);

      // Act
      var result = sut.CreateProvider();

      // Assert
      Assert.That(result, Is.SameAs(provider));
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_options_from_proxy([Frozen] ICreatesWebDriverProvidersWithOptions proxied,
                                                         object options)
    {
      // Arrange
      var sut = new ConfigurationWebDriverProviderFactoryProxy(proxied, options);

      // Act
      var result = sut.CreateProvider();

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateProvider(options, It.IsAny<IDictionary<string,object>>(), It.IsAny<IGetsBrowserFlags>()), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_capabilities_to_proxied_factory([Frozen] ICreatesWebDriverProvidersWithOptions proxied,
                                                                      ConfigurationWebDriverProviderFactoryProxy sut,
                                                                      IDictionary<string,object> capabilities)
    {
      // Act
      sut.CreateProvider(requestedCapabilities: capabilities);

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateProvider(It.IsAny<object>(), capabilities, It.IsAny<IGetsBrowserFlags>()), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_flags_provider_to_proxied_factory([Frozen] ICreatesWebDriverProvidersWithOptions proxied,
                                                                        ConfigurationWebDriverProviderFactoryProxy sut,
                                                                        IGetsBrowserFlags flagsProvider)
    {
      // Act
      sut.CreateProvider(flagsProvider: flagsProvider);

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateProvider(It.IsAny<object>(), It.IsAny<IDictionary<string,object>>(), flagsProvider), Times.Once);
    }
  }
}

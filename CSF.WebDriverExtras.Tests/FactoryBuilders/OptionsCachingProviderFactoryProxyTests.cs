using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.FactoryBuilders;
using CSF.WebDriverExtras.Factories;
using CSF.WebDriverExtras.Flags;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using OpenQA.Selenium;

namespace CSF.WebDriverExtras.Tests.FactoryBuilders
{
  [TestFixture,Parallelizable(ParallelScope.All)]
  public class OptionsCachingProviderFactoryProxyTests
  {
    [Test,AutoMoqData]
    public void CreateProvider_calls_method_from_proxied_type([Frozen] ICreatesWebDriverFromOptions proxied,
                                                              OptionsCachingDriverFactoryProxy sut)
    {
      // Act
      sut.CreateWebDriver();

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateWebDriver(It.IsAny<object>(), null, null, null), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_returns_result_from_proxied_type([Frozen] ICreatesWebDriverFromOptions proxied,
                                                                OptionsCachingDriverFactoryProxy sut,
                                                                IWebDriver driver)
    {
      // Arrange
      Mock.Get(proxied)
          .Setup(x => x.CreateWebDriver(It.IsAny<object>(), null, null, null))
          .Returns(driver);

      // Act
      var result = sut.CreateWebDriver();

      // Assert
      Assert.That(result, Is.SameAs(driver));
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_options_from_proxy([Frozen] ICreatesWebDriverFromOptions proxied,
                                                         object options)
    {
      // Arrange
      var sut = new OptionsCachingDriverFactoryProxy(proxied, options);

      // Act
      var result = sut.CreateWebDriver();

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateWebDriver(options, It.IsAny<IDictionary<string,object>>(), It.IsAny<IGetsBrowserFlags>(), It.IsAny<string>()), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_capabilities_to_proxied_factory([Frozen] ICreatesWebDriverFromOptions proxied,
                                                                      OptionsCachingDriverFactoryProxy sut,
                                                                      IDictionary<string,object> capabilities)
    {
      // Act
      sut.CreateWebDriver(requestedCapabilities: capabilities);

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateWebDriver(It.IsAny<object>(), capabilities, It.IsAny<IGetsBrowserFlags>(), It.IsAny<string>()), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_flags_provider_to_proxied_factory([Frozen] ICreatesWebDriverFromOptions proxied,
                                                                        OptionsCachingDriverFactoryProxy sut,
                                                                        IGetsBrowserFlags flagsProvider)
    {
      // Act
      sut.CreateWebDriver(flagsProvider: flagsProvider);

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateWebDriver(It.IsAny<object>(), It.IsAny<IDictionary<string,object>>(), flagsProvider, It.IsAny<string>()), Times.Once);
    }

    [Test,AutoMoqData]
    public void CreateProvider_passes_scenario_name_to_proxied_factory([Frozen] ICreatesWebDriverFromOptions proxied,
                                                                       OptionsCachingDriverFactoryProxy sut,
                                                                       string scenarioName)
    {
      // Act
      sut.CreateWebDriver(scenarioName: scenarioName);

      // Assert
      Mock.Get(proxied)
          .Verify(x => x.CreateWebDriver(It.IsAny<object>(), It.IsAny<IDictionary<string,object>>(), It.IsAny<IGetsBrowserFlags>(), scenarioName), Times.Once);
    }
  }
}

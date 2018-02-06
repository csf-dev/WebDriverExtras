using System;
using System.Collections.Generic;
using CSF.WebDriverExtras.Flags;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace CSF.WebDriverExtras.Factories
{
    /// <summary>
    /// Implementation of <see cref="T:RemoteDriverFactoryBase{TOptions}"/> for Microsoft Edge, using
    /// <see cref="LocalEdgeOptions"/>
    /// </summary>
    public class EdgeDriverFactory : RemoteDriverFactoryBase<LocalEdgeOptions>
    {
        /// <summary>
        /// Creates and returns a web driver instance.
        /// </summary>
        /// <returns>The web driver.</returns>
        /// <param name="requestedCapabilities">A collection of requested web driver capabilities.</param>
        /// <param name="options">A factory options instance.</param>
        /// <param name="flagsProvider">A service which derives a collection of browser flags for the created web driver.</param>
        /// <param name="scenarioName">The name for the current test scenario.</param>
        public override IWebDriver CreateWebDriver(IDictionary<string, object> requestedCapabilities,
                                                   LocalEdgeOptions options,
                                                   IGetsBrowserFlags flagsProvider,
                                                   string scenarioName)
        {
            var webDriver = GetWebDriver(requestedCapabilities, options);
            return WrapWithProxy(webDriver, flagsProvider);
        }

        RemoteWebDriver GetWebDriver(IDictionary<string, object> requestedCapabilities,
                                     LocalEdgeOptions options)
        {
            var driverService = GetDriverService(options);
            var edgeOptions = GetEdgeOptions(options);

            if(requestedCapabilities != null)
            {
                foreach(var cap in requestedCapabilities)
                {
                    edgeOptions.AddAdditionalCapability(cap.Key, cap.Value);
                }
            }

            var timeout = (options?.GetCommandTimeout()).GetValueOrDefault(LocalDriverOptions.DefaultCommandTimeout);
            return new EdgeDriver(driverService, edgeOptions, timeout);
        }

        EdgeDriverService GetDriverService(LocalEdgeOptions options)
        {
            EdgeDriverService output;

            if(String.IsNullOrEmpty(options?.WebDriverExecutablePath))
                output = EdgeDriverService.CreateDefaultService();
            else
                output = EdgeDriverService.CreateDefaultService(options.WebDriverExecutablePath);

            output.HideCommandPromptWindow = true;
            output.SuppressInitialDiagnosticInformation = true;

            if((options?.WebDriverPort).HasValue)
                output.Port = options.WebDriverPort.Value;

            return output;
        }

        EdgeOptions GetEdgeOptions(LocalEdgeOptions options)
        {
            return new EdgeOptions();
        }
    }
}
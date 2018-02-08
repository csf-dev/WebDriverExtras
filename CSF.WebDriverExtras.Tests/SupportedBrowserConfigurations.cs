using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CSF.WebDriverExtras.Tests
{
  public static class SupportedBrowserConfigurations
  {
    static readonly BrowserConfiguration[] supportedConfigurations = {
      // Google Chrome
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Chrome",                Version = "56.0"      },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Chrome",                Version = "64.0"      },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Chrome",                Version = null        },
      // Mozilla Firefox
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Firefox",               Version = "51.0"      },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Firefox",               Version = "58.0"      },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "Firefox",               Version = null        },
      // Internet Explorer
      new BrowserConfiguration { Platform = "Windows 7",      Name = "Internet Explorer",     Version = "8.0"       },
      new BrowserConfiguration { Platform = "Windows 7",      Name = "Internet Explorer",     Version = "9.0"       },
      new BrowserConfiguration { Platform = "Windows 7",      Name = "Internet Explorer",     Version = "10.0"      },
      new BrowserConfiguration { Platform = "Windows 7",      Name = "Internet Explorer",     Version = "11.0"      },
      // Microsoft Edge
      new BrowserConfiguration { Platform = "Windows 10",     Name = "MicrosoftEdge",         Version = "16.16299"  },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "MicrosoftEdge",         Version = "15.15063"  },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "MicrosoftEdge",         Version = "14.14393"  },
      new BrowserConfiguration { Platform = "Windows 10",     Name = "MicrosoftEdge",         Version = "13.10586"  },
      // Safari
      new BrowserConfiguration { Platform = "macOS 10.13",    Name = "Safari",                Version = "11.0"      },
      new BrowserConfiguration { Platform = "macOS 10.12",    Name = "Safari",                Version = "10.1"      },
      new BrowserConfiguration { Platform = "OS X 10.11",     Name = "Safari",                Version = "10.0"      },
    };

    /* These browser identifications were taken directly from the results of the test:
     * 
     *  BrowserIdentificationFactoryTests.GetIdentification_integration_successfully_identifies_all_browser_versions
     * 
     * This information was last updated on 2018-02-08 using a test run against Sauce Labs.
     * They represent the actual browser identification information sent back to us by the web driver.
     */
    static readonly BrowserConfiguration[] actualIdentifications = {
      new BrowserConfiguration { Name = "chrome",             Version = "56.0.2924.76" },
      new BrowserConfiguration { Name = "chrome",             Version = "64.0.3282.119" },
      new BrowserConfiguration { Name = "firefox",            Version = "51.0.1" },
      new BrowserConfiguration { Name = "firefox",            Version = String.Empty },
      new BrowserConfiguration { Name = "internet explorer",  Version = "10" },
      new BrowserConfiguration { Name = "internet explorer",  Version = "11" },
      new BrowserConfiguration { Name = "internet explorer",  Version = "8" },
      new BrowserConfiguration { Name = "internet explorer",  Version = "9" },
      new BrowserConfiguration { Name = "MicrosoftEdge",      Version = String.Empty },
      new BrowserConfiguration { Name = "safari",             Version = "10.0.1" },
      new BrowserConfiguration { Name = "safari",             Version = "10.1.2" },
      new BrowserConfiguration { Name = "safari",             Version = "13604.3.5" },
    };

    public static IReadOnlyCollection<BrowserConfiguration> GetAll() => supportedConfigurations;

    public static IEnumerable<TestCaseData> AsTestCaseData
      => GetAll().Select(x => new TestCaseData(x.Platform, x.Name, x.Version)).ToArray();

    public static IEnumerable<TestCaseData> ActualIdentifierTestCaseData
      => actualIdentifications.Select(x => new TestCaseData(x.Name, x.Version)).ToArray();

    public class BrowserConfiguration
    {
      public string Platform;
      public string Name;
      public string Version;
    }
  }
}

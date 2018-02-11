//
// BrowserIdCustomization.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Moq;
using OpenQA.Selenium;
using Ploeh.AutoFixture;

namespace CSF.WebDriverExtras.Tests.Autofixture
{
  public class WebDriverWithIdentificationCustomiszation<T> : ICustomization
    where T : class,IHasCapabilities
  {
    readonly string browser, version, requestedVersion;
    readonly PlatformType? platform;
    readonly bool hasRequestedVersion;

    public void Customize(IFixture fixture)
    {
      fixture.Customize<T>(c => {
        return c.FromFactory((PlatformType plat, string name, string ver) => CreateWebDriver(plat, name, ver));
      });
    }

    T CreateWebDriver(PlatformType plat, string name, string ver)
    {
      var driver = new Mock<T>();

      var caps = Mock.Of<ICapabilities>();

      driver.SetupGet(x => x.Capabilities).Returns(caps);

      var effectiveBrowserName = browser ?? name;
      var effectiveBrowserVersion = version ?? ver;
      var effectivePlatform = platform ?? plat;

      Mock.Get(caps).SetupGet(x => x.BrowserName).Returns(effectiveBrowserName);
      Mock.Get(caps).SetupGet(x => x.Version).Returns(effectiveBrowserVersion);
      Mock.Get(caps).SetupGet(x => x.Platform).Returns(new Platform(effectivePlatform));

      if(hasRequestedVersion)
      {
        driver
          .As<IHasRequestedVersion>()
          .SetupGet(x => x.RequestedBrowserVersion)
          .Returns(requestedVersion);
      }

      return driver.Object;
    }

    public WebDriverWithIdentificationCustomiszation(string browser,
                                    string version,
                                    PlatformType? platform,
                                    bool hasRequestedVersion,
                                    string requestedVersion)
    {
      this.browser = browser;
      this.version = version;
      this.platform = platform;
      this.hasRequestedVersion = hasRequestedVersion;
      this.requestedVersion = requestedVersion;
    }
  }
}

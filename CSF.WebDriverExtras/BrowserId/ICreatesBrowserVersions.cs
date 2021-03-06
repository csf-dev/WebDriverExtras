﻿using System;
namespace CSF.WebDriverExtras.BrowserId
{
  /// <summary>
  /// A factory service which parses strings and creates a suitable implementation of
  /// <see cref="BrowserVersion"/> from them.
  /// </summary>
  public interface ICreatesBrowserVersions
  {
    /// <summary>
    /// Creates and returns a browser version instance for a given browser.
    /// </summary>
    /// <returns>The version.</returns>
    /// <param name="versionString">The browser version string to parse.</param>
    /// <param name="browserName">The name of the browser for which to create a version.</param>
    /// <param name="requestedVersionString">A string indicating the browser version which was 'requested'.</param>
    BrowserVersion CreateVersion(string versionString, string browserName = null, string requestedVersionString = null);
  }
}

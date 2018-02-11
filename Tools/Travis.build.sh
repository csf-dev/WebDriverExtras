#!/bin/bash


setup_webdriver_environment_variables()
{
  WebDriver_SauceLabsBuildName="Travis WebDriverExtras job ${TRAVIS_JOB_NUMBER}"
  WebDriver_TunnelIdentifier="$TRAVIS_JOB_NUMBER"
}

setup_webdriver_environment_variables

export WebDriver_SauceLabsBuildName
export WebDriver_TunnelIdentifier

# The Y parameter indicates that web browser tests should run
Tools/Build.sh "Y"

exit "$?"
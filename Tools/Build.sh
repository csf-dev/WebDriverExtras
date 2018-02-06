#!/bin/bash

NUNIT_CONSOLE_VERSION="3.7.0"
NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TEST_PATTERN="CSF.WebDriverExtras.Tests.dll"

should_run_browser_tests="$1"

stop_if_failure()
{
  code="$1"
  process="$2"
  if [ "$code" -ne "0" ]
  then
    echo "The process '${process}' failed with exit code $code"
    exit "$code"
  fi
}

build_solution()
{
  echo "Building the solution ..."
  msbuild /p:Configuration=Debug CSF.WebDriverExtras.sln
  stop_if_failure $? "Build the solution"
}

run_unit_tests()
{
  echo "Running unit tests ..."
  mono "$NUNIT_PATH" "CSF.WebDriverExtras.Tests/bin/Debug/CSF.WebDriverExtras.Tests.dll" --where "cat != Browser"
  stop_if_failure $? "Run unit tests"
}

run_webbrowser_tests()
{
  echo "Running web browser tests ..."
  mono "$NUNIT_PATH" "CSF.WebDriverExtras.Tests/bin/Debug/CSF.WebDriverExtras.Tests.dll" --where "cat == Browser"
  stop_if_failure $? "Run web browser tests"
}

build_solution
run_unit_tests

if [ "$should_run_browser_tests" = "Y" ]
then
  run_webbrowser_tests
else
  echo "Skipping web browser tests"
fi

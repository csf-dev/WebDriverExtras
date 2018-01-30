#!/bin/bash

NUNIT_CONSOLE_VERSION="3.7.0"
NUNIT_PATH="./testrunner/NUnit.ConsoleRunner.${NUNIT_CONSOLE_VERSION}/tools/nunit3-console.exe"
TEST_PATTERN="CSF.WebDriverExtras.Tests.dll"

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
  test_assemblies=$(find ./Tests/ -type f -path "*/bin/Debug/*" -name "$TEST_PATTERN")
  mono "$NUNIT_PATH" $test_assemblies
  stop_if_failure $? "Run unit tests"
}

build_solution
run_unit_tests

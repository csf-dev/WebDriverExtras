@echo on

git submodule update --init --recursive

nuget restore CSF.WebDriverExtras.sln

@echo off
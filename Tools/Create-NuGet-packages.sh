#!/bin/bash

msbuild /p:Configuration=Release

if [ "$?" -ne "0" ]
then
  exit 1
fi

sn -R CSF.WebDriverExtras/bin/Release/CSF.WebDriverExtras.dll CSF-Software-OSS.snk

find . \
  -type f \
  -name "*.nuspec" \
  \! -path "./.git/*" \
  -exec nuget pack '{}' \;
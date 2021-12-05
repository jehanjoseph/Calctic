#!/bin/bash
#$1 Solution File
#$2 Configuration
#$3 Output Directory

mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/MSBuild/Current/bin/MSBuild.dll "$1" /t:Build /p:Configuration="$2" /t:PackageForAndroid /p:OutDir="$3"

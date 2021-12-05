#!/bin/bash
#$1 Solution File
#$2 Configuration
#$3 Build IPA File
#$4 Platform
#$5 IPA Output Path
#$6 Sign Identity Certificate
#$7 Provisioning Profile UUID

mono /Applications/Visual\ Studio.app/Contents/Resources/lib/monodevelop/bin/MSBuild/Current/bin/MSBuild.dll /t:Build "$1" /p:Configuration="$2" /p:BuildIpa="$3" /p:Platform="$4" /p:IpaPackageDir="$5" /p:Codesignkey="$6" /p:CodesignProvision="$7"

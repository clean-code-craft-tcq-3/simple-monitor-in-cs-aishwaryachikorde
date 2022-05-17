pushd "%~dp0"

"%programfiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\msbuild.exe" checker.csproj

popd

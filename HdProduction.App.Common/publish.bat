set version=0.1.3
dotnet pack HdProduction.App.Common.csproj -c Release /p:PackageVersion=%version%
dotnet nuget push bin\Release\*.%version%.nupkg -k oy2it2omsiiofegpydsaka6zt6dgoe5efgyporsj7elcku -s https://api.nuget.org/v3/index.json
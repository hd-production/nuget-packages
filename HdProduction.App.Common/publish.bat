dotnet pack HdProduction.App.Common.csproj -c Release /p:PackageVersion=0.1.0
dotnet nuget push bin\Release\*.nupkg -k oy2it2omsiiofegpydsaka6zt6dgoe5efgyporsj7elcku -s https://api.nuget.org/v3/index.json
# clone
This repository contains submodules and symlinks.
```PowerShell
git clone --recurse-submodules -c core.symlinks=true https://github.com/olegsych/chronology.git
```

# build
Use [Visual Studio](https://visualstudio.microsoft.com/downloads) or command line.
```PowerShell
dotnet build .\Chronology.sln
```

# test
Use Visual Studio or command line.
```PowerShell
dotnet test .\tests\Tests.csproj
```

# pull request
Pull requests are automatically validated by [AppVeyor](https://ci.appveyor.com/project/olegsych/chronology).

# continuous integration
The [AppVeyor build](https://ci.appveyor.com/project/olegsych/chronology) is automatically triggered for the master branch.
Build settings are defined in [appveyor.yml](./appveyor.yml).
NuGet and symbol packages produced by builds are immediately available from the [AppVeyor package feed](https://ci.appveyor.com/nuget/chronology).

# release
Builds are [published](https://ci.appveyor.com/project/olegsych/chronology/deployments) to the
[NuGet package feed](https://www.nuget.org/packages/chronology) when changes are ready for public consumption.

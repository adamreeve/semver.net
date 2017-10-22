param (
	[string]$BuildVersionNumber=$(throw "-BuildVersionNumber is required."),
	[string]$TagVersionNumber
)

& dotnet restore .\src\SemVer\SemVer.csproj #2>1

if ($TagVersionNumber) {
    & dotnet pack .\src\SemVer\SemVer.csproj -c Release /p:VersionPrefix="$TagVersionNumber" 2>&1
} else {
    & dotnet pack .\src\SemVer\SemVer.csproj -c Release /p:VersionPrefix="$BuildVersionNumber" 2>&1
}

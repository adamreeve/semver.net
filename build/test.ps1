# run restore on all project.json files in the src folder including 2>1 to redirect stderr to stdout for badly behaved tools
& dotnet restore 2>&1
if ($lastExitCode -ne 0) {
    $host.SetShouldExit($lastExitCode)
}

# run tests
& dotnet test .\test\SemanticVersioning.Tests\SemanticVersioning.Tests.csproj -c Release 2>&1
if ($lastExitCode -ne 0) {
    $host.SetShouldExit($lastExitCode)
}

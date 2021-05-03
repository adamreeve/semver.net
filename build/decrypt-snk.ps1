if ($env:APPVEYOR_PULL_REQUEST_NUMBER) {
    Write-Host "Build is for a pull request, not decrypting SNK file"
} else {
    Write-Host "Decrypting SNK file"
    .\secure-file\tools\secure-file -decrypt .\build\semver-key.snk.enc -secret $env:ENCRYPTION_KEY -out .\build\semver-key.snk

    Write-Host "Updating AssemblyInfo.cs"
    $assemblyInfo = '.\src\SemanticVersioning\Properties\AssemblyInfo.cs'
    (Get-Content $assemblyInfo) |
        Foreach-Object {$_ -replace 'InternalsVisibleTo\("SemanticVersioning.Tests"\)', 'InternalsVisibleTo("SemanticVersioning.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010019351d4288017757df1b69b4d0da9a775e6eec498ec93d209d6db4d62e9962476c8da01545cc47335cdc39ba803f4db368ce5f2fdd6cd395196f3328f9039dccdeb3c0f9aece7b8751cd3bc2cb2297d4f463a376eff61b7295b96af9b9faf3eef6005dc967a7a97431cc42cff72e60f05797f3e16186f8fbaf26074e96a2b5e1")'}  |
        Out-File $assemblyInfo
}

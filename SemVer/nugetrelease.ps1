# SemVer.spec was created by running "nuget spec" in this directory.
# and then editing the resulting SemVer.nuspec file.

# Properties/AssemblyInfo.cs needs to be updated with the new
# version number before making a release.
# The release notes in the .nuspec file should also be updated.

# Build package:
nuget pack SemVer.csproj -Prop Configuration=Release

# Package can then be uploaded to the NuGet Gallery with:
# nuget push SemanticVersioning.<version>.nupkg

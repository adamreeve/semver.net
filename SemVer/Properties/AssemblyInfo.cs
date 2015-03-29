using System.Reflection;
using System.Runtime.CompilerServices;

// Information about this assembly is defined by the following attributes.
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("SemVer")]
[assembly: AssemblyDescription ("This library implements the Semantic Versioning 2.0.0 specification and the version range specification used by npm.")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("Adam Reeve")]
[assembly: AssemblyProduct ("SemVer")]
[assembly: AssemblyCopyright ("Copyright 2015 Adam Reeve")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("")]

// Allow SemVer.Tests to test internal classes
[assembly:InternalsVisibleTo("SemVer.Tests")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion ("0.2.0")]

// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]


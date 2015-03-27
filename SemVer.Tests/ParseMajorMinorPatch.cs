using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class ParseMajorMinorPatch
    {
        [Theory]
        [InlineData("1.2.3", 1)]
        [InlineData(" 1.2.3 ", 1)]
        [InlineData(" 2.2.3-4 ", 2)]
        [InlineData(" 3.2.3-pre ", 3)]
        [InlineData("v5.2.3", 5)]
        [InlineData(" v8.2.3 ", 8)]
        [InlineData("\t13.2.3", 13)]
        [InlineData("=21.2.3", 21)]
        [InlineData("v=34.2.3", 34)]
        public void ParseMajorVersion(string versionString, int majorVersion)
        {
            var version = new Version(versionString);
            Assert.Equal(version.Major, majorVersion);
        }

        [Theory]
        [InlineData("1.2.3", 2)]
        [InlineData(" 1.2.3 ", 2)]
        [InlineData(" 2.2.3-4 ", 2)]
        [InlineData(" 3.2.3-pre ", 2)]
        [InlineData("v5.2.3", 2)]
        [InlineData(" v8.2.3 ", 2)]
        [InlineData("\t13.2.3", 2)]
        [InlineData("=21.2.3", 2)]
        [InlineData("v=34.2.3", 2)]
        public void ParseMinorVersion(string versionString, int minorVersion)
        {
            var version = new Version(versionString);
            Assert.Equal(version.Minor, minorVersion);
        }

        [Theory]
        [InlineData("1.2.3", 3)]
        [InlineData(" 1.2.3 ", 3)]
        [InlineData(" 2.2.3-4 ", 3)]
        [InlineData(" 3.2.3-pre ", 3)]
        [InlineData("v5.2.3", 3)]
        [InlineData(" v8.2.3 ", 3)]
        [InlineData("\t13.2.3", 3)]
        [InlineData("=21.2.3", 3)]
        [InlineData("v=34.2.3", 3)]
        public void ParsePatchVersion(string versionString, int patchVersion)
        {
            var version = new Version(versionString);
            Assert.Equal(version.Patch, patchVersion);
        }
    }
}


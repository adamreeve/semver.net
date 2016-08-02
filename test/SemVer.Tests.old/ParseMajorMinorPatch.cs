using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class ParseMajorMinorPatch
    {
        [Theory]
        [InlineData("1.2.3", 1, false)]
        [InlineData(" 1.2.3 ", 1, false)]
        [InlineData(" 2.2.3-4 ", 2, false)]
        [InlineData(" 3.2.3-pre ", 3, false)]
        [InlineData("v5.2.3", 5, false)]
        [InlineData(" v8.2.3 ", 8, false)]
        [InlineData("\t13.2.3", 13, false)]
        [InlineData("=21.2.3", 21, true)]
        [InlineData("v=34.2.3", 34, true)]
        public void ParseMajorVersion(string versionString, int majorVersion, bool loose)
        {
            var version = new Version(versionString, loose);
            Assert.Equal(version.Major, majorVersion);
        }

        [Theory]
        [InlineData("1.2.3", 2, false)]
        [InlineData(" 1.2.3 ", 2, false)]
        [InlineData(" 2.2.3-4 ", 2, false)]
        [InlineData(" 3.2.3-pre ", 2, false)]
        [InlineData("v5.2.3", 2, false)]
        [InlineData(" v8.2.3 ", 2, false)]
        [InlineData("\t13.2.3", 2, false)]
        [InlineData("=21.2.3", 2, true)]
        [InlineData("v=34.2.3", 2, true)]
        public void ParseMinorVersion(string versionString, int minorVersion, bool loose)
        {
            var version = new Version(versionString, loose);
            Assert.Equal(version.Minor, minorVersion);
        }

        [Theory]
        [InlineData("1.2.3", 3, false)]
        [InlineData(" 1.2.3 ", 3, false)]
        [InlineData(" 2.2.3-4 ", 3, false)]
        [InlineData(" 3.2.3-pre ", 3, false)]
        [InlineData("v5.2.3", 3, false)]
        [InlineData(" v8.2.3 ", 3, false)]
        [InlineData("\t13.2.3", 3, false)]
        [InlineData("=21.2.3", 3, true)]
        [InlineData("v=34.2.3", 3, true)]
        public void ParsePatchVersion(string versionString, int patchVersion, bool loose)
        {
            var version = new Version(versionString, loose);
            Assert.Equal(version.Patch, patchVersion);
        }
    }
}


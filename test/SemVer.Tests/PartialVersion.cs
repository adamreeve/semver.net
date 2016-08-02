using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class PartialVersions
    {
        [Theory]
        [InlineData("1.2.3", 1, 2, 3)]
        [InlineData("1.2", 1, 2, null)]
        [InlineData("1", 1, null, null)]
        public void TestPartialVersion(string versionString, int major, int? minor, int? patch)
        {
            var version = new PartialVersion(versionString);
            Assert.Equal(version.Major, major);
            Assert.Equal(version.Minor, minor);
            Assert.Equal(version.Patch, patch);
        }

        [Theory]
        [InlineData("1.2.x", 1, 2, null)]
        [InlineData("1.2.X", 1, 2, null)]
        [InlineData("1.2.*", 1, 2, null)]
        [InlineData("1.x", 1, null, null)]
        public void TestXVersion(string versionString, int major, int? minor, int? patch)
        {
            var version = new PartialVersion(versionString);
            Assert.Equal(version.Major, major);
            Assert.Equal(version.Minor, minor);
            Assert.Equal(version.Patch, patch);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2", "1.2.0")]
        [InlineData("1", "1.0.0")]
        public void TestToZeroVersion(string partialVersion, string fullVersion)
        {
            var version = new PartialVersion(partialVersion);
            Assert.Equal(version.ToZeroVersion(), new Version(fullVersion));
        }
    }
}


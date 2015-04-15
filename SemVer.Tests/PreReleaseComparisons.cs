using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class PreReleaseComparisons
    {
        [Fact]
        // Versions without a pre-release version are greater
        // than those with one.
        public void PreReleaseVsNoPreRelease()
        {
            var versionA = new Version("1.0.0-alpha");
            var versionB = new Version("1.0.0");
            Assert.True(versionA < versionB);
        }

        [Fact]
        public void EqualVersions()
        {
            var versionA = new Version("1.0.0-alpha.2");
            var versionB = new Version("1.0.0-alpha.2");
            Assert.True(versionA == versionB);
        }

        [Theory]
        [InlineData("1.0.0-alpha", "1.0.0-alpha.1")]
        [InlineData("1.0.0-alpha.1", "1.0.0-alpha.beta")]
        [InlineData("1.0.0-alpha.beta", "1.0.0-beta")]
        [InlineData("1.0.0-beta", "1.0.0-beta.2")]
        [InlineData("1.0.0-beta.2", "1.0.0-beta.11")]
        [InlineData("1.0.0-beta.11", "1.0.0-rc.1")]
        public void LessThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA < versionB);
        }

        [Theory]
        [InlineData("1.0.0-alpha.2", "1.0.0-alpha.1")]
        public void GreaterThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA > versionB);
        }

        [Fact]
        // Build number has no effect on order
        public void EqualExceptBuild()
        {
            var a = new Version("1.2.3-alpha.1+build.99");
            var b = new Version("1.2.3-alpha.1+build.1");
            Assert.True(a == b);
        }
    }
}

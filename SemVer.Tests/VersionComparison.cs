using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class VersionComparison
    {
        [Fact]
        public void EqualVersions()
        {
            var a = new Version("1.2.3");
            var b = new Version("1.2.3");
            Assert.True(a == b);
            Assert.True(a.Equals(b));
            var obja = a as object;
            Assert.True(obja.Equals(b));
        }

        [Theory]
        [InlineData("1.2.3", "1.2.2")]
        [InlineData("1.2.3", "1.1.3")]
        [InlineData("1.2.3", "0.2.3")]
        public void GreaterThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA > versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.4")]
        public void NotGreaterThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA > versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.2")]
        [InlineData("1.2.3", "1.1.3")]
        [InlineData("1.2.3", "0.2.3")]
        public void GreaterThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA >= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.4")]
        public void NotGreaterThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA >= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.4")]
        [InlineData("1.2.3", "1.3.3")]
        [InlineData("1.2.3", "2.2.3")]
        public void LessThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA < versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.2")]
        public void NotLessThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA < versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.4")]
        [InlineData("1.2.3", "1.3.3")]
        [InlineData("1.2.3", "2.2.3")]
        public void LessThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA <= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.2")]
        public void NotLessThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA <= versionB);
        }
    }
}

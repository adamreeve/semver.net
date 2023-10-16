using Xunit;
using Xunit.Extensions;

namespace SemanticVersioning.Tests
{
    public class PreReleaseRanges
    {
        [Theory]
        [InlineData("1.2.3-alpha.3", "1.2.3-alpha.3")]
        [InlineData(">1.2.3-alpha.3", "1.2.3-alpha.7")]
        [InlineData(">1.2.3-alpha.3", "3.4.5")]
        [InlineData(">1.2.3-alpha.3 <1.2.4", "1.2.3-alpha.7")]
        public void MatchingPreRelease(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.True(range.IsSatisfied(version));
        }

        [Theory]
        [InlineData("1.2.3-alpha.3", "1.2.3-alpha.7")]
        [InlineData("1.2.3", "1.2.3-alpha.3")]
        [InlineData("1.2.3-alpha.3", "1.2.3")]
        [InlineData(">1.2.3-alpha.3", "1.2.3-alpha.2")]
        [InlineData(">1.2.3-alpha.3", "3.4.5-alpha.9")]
        public void ExcludedPreRelease(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.False(range.IsSatisfied(version));
        }

        [Theory]
        [InlineData(">1.2.3-alpha.3", "1.2.3")]
        [InlineData(">1.2.3-alpha.3", "1.2.3-alpha.4")]
        [InlineData(">1.2.3-alpha.3", "3.4.5-alpha.1")]
        [InlineData("<1.3.0", "1.2.3-alpha.4")]
        [InlineData("<1.3.0-alpha.1", "1.2.3-alpha.4")]
        [InlineData("<1.3.0-alpha.1", "1.2.3")]
        [InlineData("<1.3.0-alpha.3", "1.3.0-alpha.2")]
        [InlineData("<=1.3.0-alpha.3", "1.3.0-alpha.3")]
        [InlineData("<=1.3.0", "1.3.0-alpha.3")]
        [InlineData(">=1.3.0", "1.3.1-alpha.1")]
        [InlineData("<1.3.0", "1.3.0-alpha.1")]
        [InlineData(">1.3", "1.4.0-alpha.1")]
        [InlineData(">=1.3", "1.3.0-alpha.1")]
        [InlineData("1.2 - 1.3", "1.2.0-alpha.1")]
        [InlineData("1.2 - 1.3", "1.3.0-alpha.1")]
        [InlineData("1.2.0 - 1.3.0", "1.2.0-alpha.1")]
        [InlineData("1.2.0 - 1.3.0", "1.3.0-alpha.1")]
        [InlineData("^0.2.3", "0.2.4-alpha")]
        public void MatchingPreReleaseWithIncludePrereleases(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.True(range.IsSatisfied(version, includePrerelease: true));
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3-alpha.3")]
        [InlineData("1.2.3-alpha.3", "1.2.3-alpha.7")]
        [InlineData("1.2.3-alpha.3", "1.2.3")]
        [InlineData(">1.2.3-alpha.3", "1.2.3-alpha.2")]
        [InlineData("<1.3.0-alpha.3", "1.3.0-alpha.4")]
        [InlineData(">=1.3.0", "1.3.0-alpha.4")]
        [InlineData("<1.3", "1.3.0-alpha.1")]
        [InlineData("~1.2.3", "1.3.0-alpha.1")]
        [InlineData("^0.2.3", "0.2.2-alpha")]
        [InlineData("^0.2.3", "0.2.3-alpha")]
        [InlineData("^0.2.3", "0.3.0-alpha")]
        public void ExcludedPreReleaseWithIncludePrereleases(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.False(range.IsSatisfied(version, includePrerelease: true));
        }

        [Theory]
        [InlineData("~1.2.3-alpha.3", "1.2.3-alpha.7")]
        [InlineData("~1.2.3-alpha.3", "1.2.5")]
        [InlineData("1.2.3-alpha.3 - 1.2.4", "1.2.3-alpha.7")]
        public void MatchingAdvancedRangePreRelease(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.True(range.IsSatisfied(version));
        }

        [Theory]
        [InlineData("~1.2.3-alpha.3", "1.2.3-alpha.2")]
        [InlineData("^0.2.3-alpha.3", "0.2.5-alpha.9")]
        public void ExcludedAdvancedRangePreRelease(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.False(range.IsSatisfied(version));
        }
    }
}

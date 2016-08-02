using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
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

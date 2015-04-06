using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class AdvancedRanges
    {
        [Theory]
        [InlineData("~1.2.3", ">=1.2.3", "<1.3.0")]
        [InlineData("~1.2", ">=1.2.0", "<1.3.0")]
        [InlineData("~1", ">=1.0.0", "<2.0.0")]
        [InlineData("~0.2.3", ">=0.2.3", "<0.3.0")]
        [InlineData("~0.2", ">=0.2.0", "<0.3.0")]
        [InlineData("~0", ">=0.0.0", "<1.0.0")]
        public void TestTildeRanges(string range,
                string comparatorStringA, string comparatorStringB)
        {
            var comparatorA = new Comparator(comparatorStringA);
            var comparatorB = new Comparator(comparatorStringB);
            var comparators = Desugarer.TildeRange(range).Item2;
            Assert.Equal(comparators.Count(), 2);
            Assert.Contains(comparatorA, comparators);
            Assert.Contains(comparatorB, comparators);
        }

        [Theory]
        [InlineData("^1.2.3", ">=1.2.3", "<2.0.0")]
        [InlineData("^0.2.3", ">=0.2.3", "<0.3.0")]
        [InlineData("^0.0.3", ">=0.0.3", "<0.0.4")]
        [InlineData("^1.2.x", ">=1.2.0", "<2.0.0")]
        [InlineData("^0.0.x", ">=0.0.0", "<0.1.0")]
        [InlineData("^0.0", ">=0.0.0", "<0.1.0")]
        [InlineData("^1.x", ">=1.0.0", "<2.0.0")]
        [InlineData("^0.x", ">=0.0.0", "<1.0.0")]
        [InlineData("^0.0.0", ">=0.0.0", "<0.0.1")]
        public void TestCaretRanges(string range,
                string comparatorStringA, string comparatorStringB)
        {
            var comparatorA = new Comparator(comparatorStringA);
            var comparatorB = new Comparator(comparatorStringB);
            var comparators = Desugarer.CaretRange(range).Item2;
            Assert.Equal(comparators.Count(), 2);
            Assert.Contains(comparatorA, comparators);
            Assert.Contains(comparatorB, comparators);
        }

        [Theory]
        [InlineData("*", new string[] { ">=0.0.0" })]
        [InlineData("1.x", new string[] { ">=1.0.0", "<2.0.0" })]
        [InlineData("1.2.x", new string[] { ">=1.2.0", "<1.3.0" })]
        [InlineData("", new string[] { ">=0.0.0" })]
        [InlineData("1", new string[] { ">=1.0.0", "<2.0.0" })]
        [InlineData("1.2", new string[] { ">=1.2.0", "<1.3.0" })]
        public void TestStarRanges(string range, string[] comparatorStrings)
        {
            var comparators = Desugarer.StarRange(range).Item2;

            Assert.Equal(comparators.Count(), comparatorStrings.Count());

            foreach (var comparatorString in comparatorStrings)
            {
                var comparator = new Comparator(comparatorString);
                Assert.Contains(comparator, comparators);
            }
        }

        [Theory]
        [InlineData("1.2.3 - 2.3.4", new string[] { ">=1.2.3", "<=2.3.4" })]
        [InlineData("1.2 - 2.3.4", new string[] { ">=1.2.0", "<=2.3.4" })]
        [InlineData("1.2.3 - 2.3", new string[] { ">=1.2.3", "<2.4.0" })]
        [InlineData("1.2.3 - 2", new string[] { ">=1.2.3", "<3.0.0" })]
        public void TestHyphenRanges(string range, string[] comparatorStrings)
        {
            var comparators = Desugarer.HyphenRange(range).Item2;

            Assert.Equal(comparators.Count(), comparatorStrings.Count());

            foreach (var comparatorString in comparatorStrings)
            {
                var comparator = new Comparator(comparatorString);
                Assert.Contains(comparator, comparators);
            }
        }

        // Range tests from npm/node-semver
        [Theory]
        [InlineData("1.0.0 - 2.0.0", "1.2.3")]
        [InlineData("^1.2.3+build", "1.2.3")]
        [InlineData("^1.2.3+build", "1.3.0")]
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "1.2.3")]
        [InlineData("1.2.3pre+asdf - 2.4.3-pre+asdf", "1.2.3")]
        [InlineData("1.2.3-pre+asdf - 2.4.3pre+asdf", "1.2.3")]
        [InlineData("1.2.3pre+asdf - 2.4.3pre+asdf", "1.2.3")]
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "1.2.3-pre.2")]
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "2.4.3-alpha")]
        [InlineData("1.2.3+asdf - 2.4.3+asdf", "1.2.3")]
        [InlineData("1.0.0", "1.0.0")]
        [InlineData(">=*", "0.2.4")]
        [InlineData("", "1.0.0")]
        [InlineData("*", "1.2.3")]
        [InlineData("*", "v1.2.3-foo")]
        [InlineData(">=1.0.0", "1.0.0")]
        [InlineData(">=1.0.0", "1.0.1")]
        [InlineData(">=1.0.0", "1.1.0")]
        [InlineData(">1.0.0", "1.0.1")]
        [InlineData(">1.0.0", "1.1.0")]
        [InlineData("<=2.0.0", "2.0.0")]
        [InlineData("<=2.0.0", "1.9999.9999")]
        [InlineData("<=2.0.0", "0.2.9")]
        [InlineData("<2.0.0", "1.9999.9999")]
        [InlineData("<2.0.0", "0.2.9")]
        [InlineData(">= 1.0.0", "1.0.0")]
        [InlineData(">= 1.0.0", "1.0.1")]
        [InlineData(">= 1.0.0", "1.1.0")]
        [InlineData("> 1.0.0", "1.0.1")]
        [InlineData("> 1.0.0", "1.1.0")]
        [InlineData("<= 2.0.0", "2.0.0")]
        [InlineData("<= 2.0.0", "1.9999.9999")]
        [InlineData("<= 2.0.0", "0.2.9")]
        [InlineData("< 2.0.0", "1.9999.9999")]
        [InlineData("<\t2.0.0", "0.2.9")]
        [InlineData(">=0.1.97", "v0.1.97")]
        [InlineData(">=0.1.97", "0.1.97")]
        [InlineData("0.1.20 || 1.2.4", "1.2.4")]
        [InlineData(">=0.2.3 || <0.0.1", "0.0.0")]
        [InlineData(">=0.2.3 || <0.0.1", "0.2.3")]
        [InlineData(">=0.2.3 || <0.0.1", "0.2.4")]
        [InlineData("||", "1.3.4")]
        [InlineData("2.x.x", "2.1.3")]
        [InlineData("1.2.x", "1.2.3")]
        [InlineData("1.2.x || 2.x", "2.1.3")]
        [InlineData("1.2.x || 2.x", "1.2.3")]
        [InlineData("x", "1.2.3")]
        [InlineData("2.*.*", "2.1.3")]
        [InlineData("1.2.*", "1.2.3")]
        [InlineData("1.2.* || 2.*", "2.1.3")]
        [InlineData("1.2.* || 2.*", "1.2.3")]
        [InlineData("*", "1.2.3")]
        [InlineData("2", "2.1.2")]
        [InlineData("2.3", "2.3.1")]
        [InlineData("~2.4", "2.4.0")]  // >=2.4.0 <2.5.0
        [InlineData("~2.4", "2.4.5")]
        [InlineData("~>3.2.1", "3.2.2")]  // >=3.2.1 <3.3.0,
        [InlineData("~1", "1.2.3")]  // >=1.0.0 <2.0.0
        [InlineData("~>1", "1.2.3")]
        [InlineData("~> 1", "1.2.3")]
        [InlineData("~1.0", "1.0.2")]  // >=1.0.0 <1.1.0,
        [InlineData("~ 1.0", "1.0.2")]
        [InlineData("~ 1.0.3", "1.0.12")]
        [InlineData(">=1", "1.0.0")]
        [InlineData(">= 1", "1.0.0")]
        [InlineData("<1.2", "1.1.1")]
        [InlineData("< 1.2", "1.1.1")]
        [InlineData("~v0.5.4-pre", "0.5.5")]
        [InlineData("~v0.5.4-pre", "0.5.4")]
        [InlineData("=0.7.x", "0.7.2")]
        [InlineData("<=0.7.x", "0.7.2")]
        [InlineData(">=0.7.x", "0.7.2")]
        [InlineData("<=0.7.x", "0.6.2")]
        [InlineData("~1.2.1 >=1.2.3", "1.2.3")]
        [InlineData("~1.2.1 =1.2.3", "1.2.3")]
        [InlineData("~1.2.1 1.2.3", "1.2.3")]
        [InlineData("~1.2.1 >=1.2.3 1.2.3", "1.2.3")]
        [InlineData("~1.2.1 1.2.3 >=1.2.3", "1.2.3")]
        [InlineData("~1.2.1 1.2.3", "1.2.3")]
        [InlineData(">=1.2.1 1.2.3", "1.2.3")]
        [InlineData("1.2.3 >=1.2.1", "1.2.3")]
        [InlineData(">=1.2.3 >=1.2.1", "1.2.3")]
        [InlineData(">=1.2.1 >=1.2.3", "1.2.3")]
        [InlineData(">=1.2", "1.2.8")]
        [InlineData("^1.2.3", "1.8.1")]
        [InlineData("^0.1.2", "0.1.2")]
        [InlineData("^0.1", "0.1.2")]
        [InlineData("^1.2", "1.4.2")]
        [InlineData("^1.2 ^1", "1.4.2")]
        [InlineData("^1.2.3-alpha", "1.2.3-pre")]
        [InlineData("^1.2.0-alpha", "1.2.0-pre")]
        [InlineData("^0.0.1-alpha", "0.0.1-beta")]
        public void SatisfiedRanges(string rangeString, string versionString)
        {
            var range = new Range(rangeString);
            var version = new Version(versionString);
            Assert.True(range.IsSatisfied(version));
        }
    }
}

using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace SemanticVersioning.Tests
{
    public class StaticRangeMethods
    {
        [Fact]
        public void TestMaxSatisfying()
        {
            var versions = new [] {
                "1.2.7",
                "1.2.8",
                "1.2.99",
                "1.2.6",
                "1.3.0",
                "1.1.0",
            };
            var max = Range.MaxSatisfying(">=1.2.7 <1.3.0", versions);
            Assert.Equal("1.2.99", max);
        }

        [Fact]
        public void TestSatisfying()
        {
            var versions = new [] {
                "1.2.7",
                "1.2.8",
                "1.2.99",
                "1.2.6",
                "1.3.0",
                "1.1.0",
            };
            var satisfying = Range.Satisfying(">=1.2.7 <1.3.0", versions).ToArray();
            Assert.Equal(3, satisfying.Count());
            Assert.Contains("1.2.7", satisfying);
            Assert.Contains("1.2.8", satisfying);
            Assert.Contains("1.2.99", satisfying);
        }

        [Theory]
        [InlineData("~1.2.3", "1.2.5", true)]
        [InlineData("^0.2.3", "0.3.4", false)]
        public void TestIsSatisfied(string range, string version, bool expectedSatisfied)
        {
            var satisfied = Range.IsSatisfied(range, version);
            Assert.Equal(expectedSatisfied, satisfied);
        }

        [Fact]
        public void TestMaxSatisfyingWithIncludePreRelease()
        {
            var versions = new [] {
                "1.2.7",
                "1.2.8",
                "1.2.98",
                "1.2.99-alpha.1",
                "1.2.6",
                "1.3.0",
                "1.1.0",
            };
            var max = Range.MaxSatisfying(">=1.2.7 <1.3.0", versions, includePrerelease: true);
            Assert.Equal("1.2.99-alpha.1", max);
        }

        [Fact]
        public void TestSatisfyingWithIncludePreRelease()
        {
            var versions = new [] {
                "1.2.7",
                "1.2.8-beta.2",
                "1.2.98",
                "1.2.99-alpha.1",
                "1.2.6",
                "1.3.0",
                "1.1.0",
            };
            var satisfying = Range.Satisfying(">=1.2.7 <1.3.0", versions, includePrerelease: true).ToArray();
            Assert.Equal(4, satisfying.Count());
            Assert.Contains("1.2.7", satisfying);
            Assert.Contains("1.2.98", satisfying);
            Assert.Contains("1.2.8-beta.2", satisfying);
            Assert.Contains("1.2.99-alpha.1", satisfying);
        }

        [Theory]
        [InlineData("~1.2.3", "1.2.5-alpha", true)]
        [InlineData("~1.2.3", "1.3.0-alpha", false)]
        [InlineData("^0.2.3", "0.2.5-alpha", true)]
        [InlineData("^0.2.3", "0.3.0-alpha", false)]
        public void TestIsSatisfiedWithIncludePreRelease(string range, string version, bool expectedSatisfied)
        {
            var satisfied = Range.IsSatisfied(range, version, includePrerelease: true);
            Assert.Equal(expectedSatisfied, satisfied);
        }
    }
}

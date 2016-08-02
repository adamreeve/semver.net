using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class StaticRangeMethods
    {
        [Fact]
        public void TestMaxSatisfying()
        {
            var versions = new [] {
                "1.2.7",
                "v1.2.8",
                "v1.2.99",
                "1.2.6",
                "v1.3.0",
                "v1.1.0",
            };
            var max = Range.MaxSatisfying(">=1.2.7 <1.3.0", versions);
            Assert.Equal("v1.2.99", max);
        }

        [Fact]
        public void TestSatisfying()
        {
            var versions = new [] {
                "1.2.7",
                "v1.2.8",
                "v1.2.99",
                "1.2.6",
                "v1.3.0",
                "v1.1.0",
            };
            var satisfying = Range.Satisfying(">=1.2.7 <1.3.0", versions).ToArray();
            Assert.Equal(3, satisfying.Count());
            Assert.Contains("1.2.7", satisfying);
            Assert.Contains("v1.2.8", satisfying);
            Assert.Contains("v1.2.99", satisfying);
        }

        [Theory]
        [InlineData("~1.2.3", "1.2.5", true)]
        [InlineData("^0.2.3", "0.3.4", false)]
        public void TestIsSatisfied(string range, string version, bool expectedSatisfied)
        {
            var satisfied = Range.IsSatisfied(range, version);
            Assert.Equal(expectedSatisfied, satisfied);
        }
    }
}

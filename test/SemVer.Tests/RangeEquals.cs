using Xunit;
using Xunit.Extensions;
using SemVer;

namespace SemVer.Tests
{
    public class RangeEquals
    {
        [Theory]
        [InlineData("~1.2.3", ">=1.2.3 <1.3.0")]
        [InlineData("~1.2.3 || =1.3.2", "=1.3.2 || >=1.2.3 <1.3.0")]
        public void EqualRanges(string a, string b)
        {
            var aRange = new Range(a);
            var bRange = new Range(b);
            Assert.True(aRange.Equals(bRange));
        }

        [Theory]
        [InlineData(">1.0.0 <2.0.0", ">1.0.0 <3.0.0")]
        [InlineData("~1.2.3 || =1.3.2", ">=1.2.3 <1.3.0")]
        [InlineData(">1.2.3", ">=1.2.3")]
        [InlineData(">1.2.3", ">1.2.0")]
        public void NotEqualRanges(string a, string b)
        {
            var aRange = new Range(a);
            var bRange = new Range(b);
            Assert.False(aRange.Equals(bRange));
        }

        [Theory]
        [InlineData("~1.2.3", ">=1.2.3 <1.3.0")]
        [InlineData("~1.2.3 || =1.3.2", "=1.3.2 || >=1.2.3 <1.3.0")]
        public void EqualHashes(string a, string b)
        {
            var aRange = new Range(a);
            var bRange = new Range(b);
            Assert.True(aRange.GetHashCode().Equals(bRange.GetHashCode()));
        }
    }
}


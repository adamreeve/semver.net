using Xunit;

namespace SemVer.Tests
{
    public class RangeOperations
    {
        [Theory]
        [InlineData("~1.2.3", ">=1.2.0 < 1.2.6", ">=1.2.3 < 1.2.6")]
        [InlineData("~1.2.3", "~1.3.2", "<0.0.0")]
        [InlineData("~1.2.3", "=1.2.7", "=1.2.7")]
        [InlineData("~1.2.3", "=1.3.2", "<0.0.0")]
        [InlineData("=1.2.3", "=1.2.3", "=1.2.3")]
        [InlineData("=1.2.3", "=1.2.4", "<0.0.0")]
        [InlineData("~1.2.3 || ~1.3.2", ">=1.2.9 < 1.3.8", ">=1.2.9 < 1.3.0 || >=1.3.2 < 1.3.8")]
        public void Intersect(string a, string b, string intersect)
        {
            var rangeA = new Range(a);
            var rangeB = new Range(b);
            var expected = new Range(intersect);

            var rangeIntersect = rangeA.Intersect(rangeB);
            Assert.Equal(expected, rangeIntersect);
        }
    }
}

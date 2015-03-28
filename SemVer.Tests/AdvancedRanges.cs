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
            var comparators = Desugarer.TildeRange(range).ToArray();
            Assert.NotNull(comparators);
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
            var comparators = Desugarer.CaretRange(range).ToArray();
            Assert.NotNull(comparators);
            Assert.Equal(comparators.Count(), 2);
            Assert.Contains(comparatorA, comparators);
            Assert.Contains(comparatorB, comparators);
        }
    }
}

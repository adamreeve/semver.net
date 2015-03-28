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
        public void TestStarRanges(string range,
                string[] comparatorStrings)
        {
            var comparators = Desugarer.StarRange(range).ToArray();

            Assert.Equal(comparators.Count(), comparatorStrings.Count());

            foreach (var comparatorString in comparatorStrings)
            {
                var comparator = new Comparator(comparatorString);
                Assert.Contains(comparator, comparators);
            }
        }
    }
}

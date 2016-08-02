using Xunit;

namespace SemVer.Tests
{
    public class SimpleRanges
    {
        [Fact]
        public void Test01()
        {
            var range = new Range(">=1.2.7 <1.3.0");
            Assert.True(range.IsSatisfied(new Version("1.2.7")));
            Assert.True(range.IsSatisfied(new Version("1.2.8")));
            Assert.True(range.IsSatisfied(new Version("1.2.99")));
            Assert.False(range.IsSatisfied(new Version("1.2.6")));
            Assert.False(range.IsSatisfied(new Version("1.3.0")));
            Assert.False(range.IsSatisfied(new Version("1.1.0")));
        }

        [Fact]
        public void Test02()
        {
            var range = new Range("1.2.7 || >=1.2.9 <2.0.0");
            Assert.True(range.IsSatisfied(new Version("1.2.7")));
            Assert.True(range.IsSatisfied(new Version("1.2.9")));
            Assert.True(range.IsSatisfied(new Version("1.4.6")));
            Assert.False(range.IsSatisfied(new Version("1.2.8")));
            Assert.False(range.IsSatisfied(new Version("2.0.0")));
        }

        [Fact]
        public void TestMaxSatisfying()
        {
            var versions = new [] {
                new Version("1.2.7"),
                new Version("1.2.8"),
                new Version("1.2.99"),
                new Version("1.2.6"),
                new Version("1.3.0"),
                new Version("1.1.0"),
            };
            var range = new Range(">=1.2.7 <1.3.0");
            Assert.Equal(
                    new Version("1.2.99"),
                    range.MaxSatisfying(versions));
        }

        [Fact]
        public void TestStringMaxSatisfying()
        {
            var versions = new [] {
                "=1.2.7",
                "v1.2.8",
                "v1.2.99",
                "=1.2.6",
                "v1.3.0",
                "v1.1.0",
            };
            var range = new Range(">=1.2.7 <1.3.0");
            Assert.Equal("v1.2.99", range.MaxSatisfying(versions));
        }

        [Fact]
        public void TestNoMaxSatisfying()
        {
            var versions = new [] {
                "1.2.7",
                "1.3.0",
            };
            var range = new Range(">=1.2.9 <1.3.0");
            Assert.Null(range.MaxSatisfying(versions));
        }

        [Fact]
        public void RangeToString()
        {
            var range = new Range(">=1.2.7 <1.3.0");
            Assert.Equal(">=1.2.7 <1.3.0", range.ToString());
        }
    }
}

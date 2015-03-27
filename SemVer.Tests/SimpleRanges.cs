using Xunit;

namespace SemVer.Tests
{
    public class SimpleRanges
    {
        [Fact]
        public void Test01()
        {
            var range = new Range(">=1.2.7 <1.3.0");
            Assert.True(range.Match(new Version("1.2.7")));
            Assert.True(range.Match(new Version("1.2.8")));
            Assert.True(range.Match(new Version("1.2.99")));
            Assert.False(range.Match(new Version("1.2.6")));
            Assert.False(range.Match(new Version("1.3.0")));
            Assert.False(range.Match(new Version("1.1.0")));
        }

        [Fact]
        public void Test02()
        {
            var range = new Range("1.2.7 || >=1.2.9 <2.0.0");
            Assert.True(range.Match(new Version("1.2.7")));
            Assert.True(range.Match(new Version("1.2.9")));
            Assert.True(range.Match(new Version("1.4.6")));
            Assert.False(range.Match(new Version("1.2.8")));
            Assert.False(range.Match(new Version("2.0.0")));
        }

        [Fact]
        public void TestSelect()
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
                    range.Select(versions));
        }
    }
}

using System;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace SemanticVersioning.Tests
{
    public class VersionComparison
    {
        [Fact]
        public void EqualVersions()
        {
            var a = new Version("1.2.3");
            var b = new Version("1.2.3");
            Assert.True(a == b);
            Assert.True(a.Equals(b));
            var obja = a as object;
            Assert.True(obja.Equals(b));
        }

        [Theory]
        [InlineData("1.2.3", "1.2.2")]
        [InlineData("1.2.3", "1.1.3")]
        [InlineData("1.2.3", "0.2.3")]
        public void GreaterThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA > versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.4")]
        public void NotGreaterThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA > versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.2")]
        [InlineData("1.2.3", "1.1.3")]
        [InlineData("1.2.3", "0.2.3")]
        public void GreaterThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA >= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.4")]
        public void NotGreaterThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA >= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.4")]
        [InlineData("1.2.3", "1.3.3")]
        [InlineData("1.2.3", "2.2.3")]
        public void LessThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA < versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.2")]
        public void NotLessThan(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA < versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3", "1.2.4")]
        [InlineData("1.2.3", "1.3.3")]
        [InlineData("1.2.3", "2.2.3")]
        public void LessThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.True(versionA <= versionB);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.2")]
        public void NotLessThanOrEqual(string a, string b)
        {
            var versionA = new Version(a);
            var versionB = new Version(b);
            Assert.False(versionA <= versionB);
        }

        [Fact]
        public void CompareTo()
        {
            var actual =
                new []
                {
                    "1.5.0",
                    "0.0.4",
                    "0.1.0",
                    "0.1.1",
                    "0.0.2",
                    "1.0.0"
                }
                .Select(v => new Version(v))
                .Cast<IComparable>()
                .OrderBy(x => x)
                .Select(x => x.ToString());

            var expected =
                new[]
                {
                    "0.0.2",
                    "0.0.4",
                    "0.1.0",
                    "0.1.1",
                    "1.0.0",
                    "1.5.0"
                };

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void CompareToNull()
        {
            var version = (IComparable) new Version("1.0.0");
            var actual = version.CompareTo(null);
            Assert.Equal(1, actual);
        }
        
        [Fact]
        public void CompareToDifferentType()
        {
            var version = (IComparable) new Version("1.0.0");
            Assert.Throws<ArgumentException>(() => version.CompareTo(new Object()));
        }
        
        // Comparison tests from npm/node-semver
        [Theory]
        [InlineData("0.0.0", "0.0.0-foo")]
        [InlineData("0.0.1", "0.0.0")]
        [InlineData("1.0.0", "0.9.9")]
        [InlineData("0.10.0", "0.9.0")]
        [InlineData("0.99.0", "0.10.0")]
        [InlineData("2.0.0", "1.2.3")]
        [InlineData("1.2.3", "1.2.3-asdf")]
        [InlineData("1.2.3", "1.2.3-4")]
        [InlineData("1.2.3", "1.2.3-4-foo")]
        [InlineData("1.2.3-5-foo", "1.2.3-5")]
        [InlineData("1.2.3-5", "1.2.3-4")]
        [InlineData("1.2.3-5-foo", "1.2.3-5-Foo")]
        [InlineData("3.0.0", "2.7.2+asdf")]
        [InlineData("1.2.3-a.10", "1.2.3-a.5")]
        [InlineData("1.2.3-a.b", "1.2.3-a.5")]
        [InlineData("1.2.3-a.b", "1.2.3-a")]
        [InlineData("1.2.3-a.b.c.10.d.5", "1.2.3-a.b.c.5.d.100")]
        [InlineData("1.2.3-r2", "1.2.3-r100")]
        [InlineData("1.2.3-r100", "1.2.3-R2")]
        public void Comparisons(string v0s, string v1s)
        {
            var v0 = new Version(v0s);
            var v1 = new Version(v1s);
            Assert.True(v0 > v1);
            Assert.True(v1 < v0);
            Assert.False(v1 > v0);
            Assert.False(v0 < v1);
            Assert.Equal(v0, v0);
            Assert.Equal(v1, v1);
            Assert.NotEqual(v0, v1);
        }

        // Equality tests from npm/node-semver
        [Theory]
        [InlineData("1.2.3", "1.2.3", false)]
        [InlineData("1.2.3", "=1.2.3", true)]
        [InlineData("1.2.3", " 1.2.3", true)]
        [InlineData("1.2.3", "= 1.2.3", true)]
        [InlineData("1.2.3", " =1.2.3", true)]
        [InlineData("1.2.3", " v 1.2.3", true)]
        [InlineData("1.2.3", " = 1.2.3", true)]
        [InlineData("1.2.3-0", "1.2.3-0", false)]
        [InlineData("1.2.3-0", "=1.2.3-0", true)]
        [InlineData("1.2.3-0", " 1.2.3-0", true)]
        [InlineData("1.2.3-0", "= 1.2.3-0", true)]
        [InlineData("1.2.3-0", " =1.2.3-0", true)]
        [InlineData("1.2.3-0", " v 1.2.3-0", true)]
        [InlineData("1.2.3-0", " = 1.2.3-0", true)]
        [InlineData("1.2.3-1", "1.2.3-1", false)]
        [InlineData("1.2.3-1", "=1.2.3-1", true)]
        [InlineData("1.2.3-1", " 1.2.3-1", true)]
        [InlineData("1.2.3-1", "= 1.2.3-1", true)]
        [InlineData("1.2.3-1", " =1.2.3-1", true)]
        [InlineData("1.2.3-1", " v 1.2.3-1", true)]
        [InlineData("1.2.3-1", " = 1.2.3-1", true)]
        [InlineData("1.2.3-beta", "1.2.3-beta", false)]
        [InlineData("1.2.3-beta", "=1.2.3-beta", true)]
        [InlineData("1.2.3-beta", " 1.2.3-beta", true)]
        [InlineData("1.2.3-beta", "= 1.2.3-beta", true)]
        [InlineData("1.2.3-beta", " =1.2.3-beta", true)]
        [InlineData("1.2.3-beta", " v 1.2.3-beta", true)]
        [InlineData("1.2.3-beta", " = 1.2.3-beta", true)]
        [InlineData("1.2.3-beta+build", " = 1.2.3-beta+otherbuild", true)]
        [InlineData("1.2.3+build", " = 1.2.3+otherbuild", true)]
        [InlineData("1.2.3-beta+build", "1.2.3-beta+otherbuild", false)]
        [InlineData("1.2.3+build", "1.2.3+otherbuild", false)]
        public void Equality(string v0s, string v1s, bool loose)
        {
            var v0 = new Version(v0s, false);
            var v1 = new Version(v1s, loose);

            Assert.True(v0.Equals(v1));
            Assert.True(v0 == v1);
            Assert.False(v0 != v1);
            Assert.False(v0 > v1);
            Assert.True(v0 >= v1);
            Assert.False(v0 < v1);
            Assert.True(v0 <= v1);
        }

        [Fact]
        public void EqualHashCode()
        {
            var a = new Version("1.2.3");
            var b = new Version("1.2.3");
            Assert.True(a.GetHashCode() == b.GetHashCode());
        }

        [Fact]
        public void DifferentHashCode()
        {
            var a = new Version("1.2.3");
            var b = new Version("1.2.4");
            Assert.False(a.GetHashCode() == b.GetHashCode());
        }

        [Fact]
        public void NullEqualsNull()
        {
            Version a = null;
            Version b = null;
            Assert.True(a == b);
            Assert.True(a >= b);
            Assert.True(a <= b);
            Assert.False(a != b);
            Assert.False(a > b);
            Assert.False(a < b);
        }

        [Fact]
        public void NullNotEqualValidVersion()
        {
            Version a = null;
            Version b = new Version("1.2.3");
            Assert.False(a == b);
            Assert.False(b == a);
            Assert.True(a != b);
            Assert.True(b != a);
        }

        [Fact]
        public void NullLessThanValidVersion()
        {
            Version a = null;
            Version b = new Version("1.2.3");
            Assert.True(a < b);
            Assert.True(a <= b);
            Assert.True(b > a);
            Assert.True(b >= a);
        }
    }
}

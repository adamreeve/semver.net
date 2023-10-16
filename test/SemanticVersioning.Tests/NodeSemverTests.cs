using Xunit;

namespace SemanticVersioning.Tests
{
    public class NodeSemverTest
    {
        // test set are from node-semver
        // Copyright (c) Isaac Z. Schlueter and Contributors
        // Originally under The ISC License
        // https://github.com/npm/node-semver/blob/09c69e23cdf6c69c51f83635482fff89ab2574e3/test/fixtures/range-include.js
        [Theory]
        [InlineData("1.0.0 - 2.0.0", "1.2.3")]
        [InlineData("^1.2.3+build", "1.2.3")]
        [InlineData("^1.2.3+build", "1.3.0")]
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "1.2.3")]
        //[InlineData("1.2.3pre+asdf - 2.4.3-pre+asdf", "1.2.3", true)] // loose
        //[InlineData("1.2.3-pre+asdf - 2.4.3pre+asdf", "1.2.3", true)] // loose
        //[InlineData("1.2.3pre+asdf - 2.4.3pre+asdf", "1.2.3", true)] // loose
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "1.2.3-pre.2")]
        [InlineData("1.2.3-pre+asdf - 2.4.3-pre+asdf", "2.4.3-alpha")]
        [InlineData("1.2.3+asdf - 2.4.3+asdf", "1.2.3")]
        [InlineData("1.0.0", "1.0.0")]
        [InlineData(">=*", "0.2.4")]
        [InlineData("", "1.0.0")]
        //[InlineData("*", "1.2.3", {})] // loose
        //[InlineData("*", "v1.2.3", { loose: 123 })] // loose
        //[InlineData(">=1.0.0", "1.0.0", /asdf/)] // loose
        //[InlineData(">=1.0.0", "1.0.1", { loose: null })] // loose
        //[InlineData(">=1.0.0", "1.1.0", { loose: 0 })] // loose
        //[InlineData(">1.0.0", "1.0.1", { loose: undefined })] // loose
        [InlineData(">1.0.0", "1.1.0")]
        [InlineData("<=2.0.0", "2.0.0")]
        [InlineData("<=2.0.0", "1.9999.9999")]
        [InlineData("<=2.0.0", "0.2.9")]
        [InlineData("<2.0.0", "1.9999.9999")]
        [InlineData("<2.0.0", "0.2.9")]
        [InlineData(">= 1.0.0", "1.0.0")]
        [InlineData(">=  1.0.0", "1.0.1")]
        [InlineData(">=   1.0.0", "1.1.0")]
        [InlineData("> 1.0.0", "1.0.1")]
        [InlineData(">  1.0.0", "1.1.0")]
        [InlineData("<=   2.0.0", "2.0.0")]
        [InlineData("<= 2.0.0", "1.9999.9999")]
        [InlineData("<=  2.0.0", "0.2.9")]
        [InlineData("<    2.0.0", "1.9999.9999")]
        [InlineData("<\t2.0.0", "0.2.9")]
        [InlineData(">=0.1.97", "0.1.97", true)]
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
        [InlineData("~0.0.1", "0.0.1")]
        [InlineData("~0.0.1", "0.0.2")]
        [InlineData("~x", "0.0.9")] // >=2.4.0 <2.5.0
        [InlineData("~2", "2.0.9")] // >=2.4.0 <2.5.0
        [InlineData("~2.4", "2.4.0")] // >=2.4.0 <2.5.0
        [InlineData("~2.4", "2.4.5")]
        //[InlineData("~>3.2.1", "3.2.2")] // >=3.2.1 <3.3.0, // ~> is not supported
        [InlineData("~1", "1.2.3")] // >=1.0.0 <2.0.0
        //[InlineData("~>1", "1.2.3")] // ~> is not supported
        //[InlineData("~> 1", "1.2.3")] // ~> is not supported
        [InlineData("~1.0", "1.0.2")] // >=1.0.0 <1.1.0,
        [InlineData("~ 1.0", "1.0.2")]
        [InlineData("~ 1.0.3", "1.0.12")]
        //[InlineData("~ 1.0.3alpha", "1.0.12", { loose: true })] // loose
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
        [InlineData(">=1.2.1 1.2.3", "1.2.3")]
        [InlineData("1.2.3 >=1.2.1", "1.2.3")]
        [InlineData(">=1.2.3 >=1.2.1", "1.2.3")]
        [InlineData(">=1.2.1 >=1.2.3", "1.2.3")]
        [InlineData(">=1.2", "1.2.8")]
        [InlineData("^1.2.3", "1.8.1")]
        [InlineData("^0.1.2", "0.1.2")]
        [InlineData("^0.1", "0.1.2")]
        [InlineData("^0.0.1", "0.0.1")]
        [InlineData("^1.2", "1.4.2")]
        [InlineData("^1.2 ^1", "1.4.2")]
        [InlineData("^1.2.3-alpha", "1.2.3-pre")]
        [InlineData("^1.2.0-alpha", "1.2.0-pre")]
        [InlineData("^0.0.1-alpha", "0.0.1-beta")]
        [InlineData("^0.0.1-alpha", "0.0.1")]
        [InlineData("^0.1.1-alpha", "0.1.1-beta")]
        [InlineData("^x", "1.2.3")]
        [InlineData("x - 1.0.0", "0.9.7")]
        [InlineData("x - 1.x", "0.9.7")]
        [InlineData("1.0.0 - x", "1.9.7")]
        [InlineData("1.x - x", "1.9.7")]
        [InlineData("<=7.x", "7.9.9")]
        [InlineData("2.x", "2.0.0-pre.0", true)]
        [InlineData("2.x", "2.1.0-pre.0", true)]
        [InlineData("1.1.x", "1.1.0-a", true)]
        [InlineData("1.1.x", "1.1.1-a", true)]
        [InlineData("*", "1.0.0-rc1", true)]
        [InlineData("^1.0.0-0", "1.0.1-rc1", true)]
        [InlineData("^1.0.0-rc2", "1.0.1-rc1", true)]
        [InlineData("^1.0.0", "1.0.1-rc1", true)]
        [InlineData("^1.0.0", "1.1.0-rc1", true)]
        [InlineData("1 - 2", "2.0.0-pre", true)]
        [InlineData("1 - 2", "1.0.0-pre", true)]
        [InlineData("1.0 - 2", "1.0.0-pre", true)]

        [InlineData("=0.7.x", "0.7.0-asdf", true)]
        [InlineData(">=0.7.x", "0.7.0-asdf", true)]
        [InlineData("<=0.7.x", "0.7.0-asdf", true)]

        [InlineData(">=1.0.0 <=1.1.0", "1.1.0-pre", true)]
        public void TestPositive(string range, string version, bool includePrerelease = false)
        {
            Assert.True(new Range(range).IsSatisfied(new Version(version), includePrerelease));
        }
        
        // test set are from node-semver
        // Copyright (c) Isaac Z. Schlueter and Contributors
        // Originally under The ISC License
        // https://github.com/npm/node-semver/blob/09c69e23cdf6c69c51f83635482fff89ab2574e3/test/fixtures/range-exclude.js
        [Theory]
        [InlineData("1.0.0 - 2.0.0", "2.2.3")]
        [InlineData("1.2.3+asdf - 2.4.3+asdf", "1.2.3-pre.2")]
        [InlineData("1.2.3+asdf - 2.4.3+asdf", "2.4.3-alpha")]
        [InlineData("^1.2.3+build", "2.0.0")]
        [InlineData("^1.2.3+build", "1.2.0")]
        [InlineData("^1.2.3", "1.2.3-pre")]
        [InlineData("^1.2", "1.2.0-pre")]
        [InlineData(">1.2", "1.3.0-beta")]
        [InlineData("<=1.2.3", "1.2.3-beta")]
        [InlineData("^1.2.3", "1.2.3-beta")]
        [InlineData("=0.7.x", "0.7.0-asdf")]
        [InlineData(">=0.7.x", "0.7.0-asdf")]
        [InlineData("<=0.7.x", "0.7.0-asdf")]
        //[InlineData("1", "1.0.0beta", { loose: 420 })] // loose
        //[InlineData("<1", "1.0.0beta", true)] // loose
        //[InlineData("< 1", "1.0.0beta", true)] // loose
        [InlineData("1.0.0", "1.0.1")]
        [InlineData(">=1.0.0", "0.0.0")]
        [InlineData(">=1.0.0", "0.0.1")]
        [InlineData(">=1.0.0", "0.1.0")]
        [InlineData(">1.0.0", "0.0.1")]
        [InlineData(">1.0.0", "0.1.0")]
        [InlineData("<=2.0.0", "3.0.0")]
        [InlineData("<=2.0.0", "2.9999.9999")]
        [InlineData("<=2.0.0", "2.2.9")]
        [InlineData("<2.0.0", "2.9999.9999")]
        [InlineData("<2.0.0", "2.2.9")]
        //[InlineData(">=0.1.97", "v0.1.93", true)] // loose
        [InlineData(">=0.1.97", "0.1.93")]
        [InlineData("0.1.20 || 1.2.4", "1.2.3")]
        [InlineData(">=0.2.3 || <0.0.1", "0.0.3")]
        [InlineData(">=0.2.3 || <0.0.1", "0.2.2")]
        //[InlineData("2.x.x", "1.1.3", { loose: NaN })] // loose
        [InlineData("2.x.x", "3.1.3")]
        [InlineData("1.2.x", "1.3.3")]
        [InlineData("1.2.x || 2.x", "3.1.3")]
        [InlineData("1.2.x || 2.x", "1.1.3")]
        [InlineData("2.*.*", "1.1.3")]
        [InlineData("2.*.*", "3.1.3")]
        [InlineData("1.2.*", "1.3.3")]
        [InlineData("1.2.* || 2.*", "3.1.3")]
        [InlineData("1.2.* || 2.*", "1.1.3")]
        [InlineData("2", "1.1.2")]
        [InlineData("2.3", "2.4.1")]
        [InlineData("~0.0.1", "0.1.0-alpha")]
        [InlineData("~0.0.1", "0.1.0")]
        [InlineData("~2.4", "2.5.0")] // >=2.4.0 <2.5.0
        [InlineData("~2.4", "2.3.9")]
        //[InlineData("~>3.2.1", "3.3.2")] // >=3.2.1 <3.3.0 // ~> is not supported
        //[InlineData("~>3.2.1", "3.2.0")] // >=3.2.1 <3.3.0 // ~> is not supported
        [InlineData("~1", "0.2.3")] // >=1.0.0 <2.0.0
        //[InlineData("~>1", "2.2.3")] // ~> is not supported
        [InlineData("~1.0", "1.1.0")] // >=1.0.0 <1.1.0
        [InlineData("<1", "1.0.0")]
        [InlineData(">=1.2", "1.1.1")]
        //[InlineData("1", "2.0.0beta", true)] // loose
        [InlineData("~v0.5.4-beta", "0.5.4-alpha")]
        [InlineData("=0.7.x", "0.8.2")]
        [InlineData(">=0.7.x", "0.6.2")]
        [InlineData("<0.7.x", "0.7.2")]
        [InlineData("<1.2.3", "1.2.3-beta")]
        [InlineData("=1.2.3", "1.2.3-beta")]
        [InlineData(">1.2", "1.2.8")]
        [InlineData("^0.0.1", "0.0.2-alpha")]
        [InlineData("^0.0.1", "0.0.2")]
        [InlineData("^1.2.3", "2.0.0-alpha")]
        [InlineData("^1.2.3", "1.2.2")]
        [InlineData("^1.2", "1.1.9")]
        //[InlineData("*", "v1.2.3-foo", true)] // loose

        // invalid versions never satisfy, but shouldn"t throw
        // C#: Invalid version should throw exception
        //[InlineData("*", "not a version")]
        //[InlineData(">=2", "glorp")]
        //[InlineData(">=2", false)]

        [InlineData("2.x", "3.0.0-pre.0", true)]
        [InlineData("^1.0.0", "1.0.0-rc1", true)]
        [InlineData("^1.0.0", "2.0.0-rc1", true)]
        [InlineData("^1.2.3-rc2", "2.0.0", true)]
        [InlineData("^1.0.0", "2.0.0-rc1")]

        [InlineData("1 - 2", "3.0.0-pre", true)]
        [InlineData("1 - 2", "2.0.0-pre")]
        [InlineData("1 - 2", "1.0.0-pre")]
        [InlineData("1.0 - 2", "1.0.0-pre")]

        [InlineData("1.1.x", "1.0.0-a")]
        [InlineData("1.1.x", "1.1.0-a")]
        [InlineData("1.1.x", "1.2.0-a")]
        [InlineData("1.1.x", "1.2.0-a", true)]
        [InlineData("1.1.x", "1.0.0-a", true)]
        [InlineData("1.x", "1.0.0-a")]
        [InlineData("1.x", "1.1.0-a")]
        [InlineData("1.x", "1.2.0-a")]
        [InlineData("1.x", "0.0.0-a", true)]
        [InlineData("1.x", "2.0.0-a", true)]

        [InlineData(">=1.0.0 <1.1.0", "1.1.0")]
        [InlineData(">=1.0.0 <1.1.0", "1.1.0", true)]
        [InlineData(">=1.0.0 <1.1.0", "1.1.0-pre")]
        [InlineData(">=1.0.0 <1.1.0-pre", "1.1.0-pre")]
        public void TestNegative(string range, string version, bool includePrerelease = false)
        {
            Assert.False(new Range(range).IsSatisfied(new Version(version), includePrerelease));
        }
    }
}

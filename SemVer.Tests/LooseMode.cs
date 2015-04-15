using System;
using Xunit;
using Xunit.Extensions;

namespace SemVer.Tests
{
    public class LooseMode
    {
        [Theory]
        [InlineData("=1.2.3", "1.2.3")]
        [InlineData("v 1.2.3", "1.2.3")]
        [InlineData("1.2.3alpha", "1.2.3-alpha")]
        // SemVer spec is ambiguous about whether 01 in a prerelease identifier
        // means the identifier is invalid or must be treated as alphanumeric,
        // but consensus seems to be that it is invalid (this is the behaviour of node-semver).
        [InlineData("1.2.3-01", "1.2.3-1")]
        public void Versions(string looseVersionString, string strictVersionString)
        {
            var looseVersion = new Version(looseVersionString, true);

            // Loose version is equivalent to strict version
            var strictVersion = new Version(strictVersionString);
            Assert.Equal(strictVersion.Clean(), looseVersion.Clean());
            Assert.Equal(strictVersion, looseVersion);

            // Loose version should be invalid in strict mode
            Assert.Throws<ArgumentException>(() => new Version(looseVersionString));
        }

        [Theory]
        [InlineData(">=1.2.3", "=1.2.3")]
        public void Ranges(string rangeString, string versionString)
        {
            var range = new Range(rangeString);

            // Version doesn't satisfy range in strict mode
            Assert.False(range.IsSatisfied(versionString, false));

            // Version satisfies range in loose mode
            Assert.True(range.IsSatisfied(versionString, true));
        }
    }
}


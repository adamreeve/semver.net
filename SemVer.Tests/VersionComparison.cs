using Xunit;
using System;

namespace SemVer.Tests
{
    public class VersionComparison
    {
        [Fact]
        public void EqualVersions()
        {
            var a = new SemVer.Version("1.2.3");
            var b = new SemVer.Version("1.2.3");
            Assert.True(a == b);
            Assert.True(a.Equals(b));
            var obja = a as object;
            Assert.True(obja.Equals(b));
        }

        [Fact]
        public void GreaterThan()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.2"),
                Tuple.Create("1.2.3", "1.1.3"),
                Tuple.Create("1.2.3", "0.2.3"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.True(a > b);
            }
        }

        [Fact]
        public void NotGreaterThan()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.3"),
                Tuple.Create("1.2.3", "1.2.4"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.False(a > b);
            }
        }

        [Fact]
        public void GreaterThanOrEqual()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.3"),
                Tuple.Create("1.2.3", "1.2.2"),
                Tuple.Create("1.2.3", "1.1.3"),
                Tuple.Create("1.2.3", "0.2.3"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.True(a >= b);
            }
        }

        [Fact]
        public void NotGreaterThanOrEqual()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.4"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.False(a >= b);
            }
        }

        [Fact]
        public void Lesshan()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.4"),
                Tuple.Create("1.2.3", "1.3.3"),
                Tuple.Create("1.2.3", "2.2.3"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.True(a < b);
            }
        }

        [Fact]
        public void NotLesshan()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.3"),
                Tuple.Create("1.2.3", "1.2.2"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.False(a < b);
            }
        }

        [Fact]
        public void LesshanOrEqual()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.3"),
                Tuple.Create("1.2.3", "1.2.4"),
                Tuple.Create("1.2.3", "1.3.3"),
                Tuple.Create("1.2.3", "2.2.3"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.True(a <= b);
            }
        }

        [Fact]
        public void NotLesshanOrEqual()
        {
            var testCases = new Tuple<string, string>[] {
                Tuple.Create("1.2.3", "1.2.2"),
            };

            foreach (var testCase in testCases)
            {
                var a = new SemVer.Version(testCase.Item1);
                var b = new SemVer.Version(testCase.Item2);
                Assert.False(a <= b);
            }
        }
    }
}

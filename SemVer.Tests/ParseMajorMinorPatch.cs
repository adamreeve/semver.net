using Xunit;
using System;

namespace SemVer.Tests
{
    public class ParseMajorMinorPatch
    {
        [Fact]
        public void ParseMajorVersion ()
        {
            var testCases = new Tuple<string, int>[] {
                Tuple.Create("1.2.3", 1),
                Tuple.Create(" 1.2.3 ", 1),
                Tuple.Create(" 2.2.3-4 ", 2),
                Tuple.Create(" 3.2.3-pre ", 3),
                Tuple.Create("v5.2.3", 5),
                Tuple.Create(" v8.2.3 ", 8),
                Tuple.Create("\t13.2.3", 13),
                Tuple.Create("=21.2.3", 21),
                Tuple.Create("v=34.2.3", 34),
            };

            foreach (var testCase in testCases)
            {
                var version = new SemVer.Version(testCase.Item1);
                Assert.Equal(version.Major, testCase.Item2);
            }
        }

        [Fact]
        public void ParseMinorVersion ()
        {
            var testCases = new Tuple<string, int>[] {
                Tuple.Create("1.2.3", 2),
                Tuple.Create(" 1.2.3 ", 2),
                Tuple.Create(" 2.2.3-4 ", 2),
                Tuple.Create(" 3.2.3-pre ", 2),
                Tuple.Create("v5.2.3", 2),
                Tuple.Create(" v8.2.3 ", 2),
                Tuple.Create("\t13.2.3", 2),
                Tuple.Create("=21.2.3", 2),
                Tuple.Create("v=34.2.3", 2),
            };

            foreach (var testCase in testCases)
            {
                var version = new SemVer.Version(testCase.Item1);
                Assert.Equal(version.Minor, testCase.Item2);
            }
        }

        [Fact]
        public void ParsePatchVersion ()
        {
            var testCases = new Tuple<string, int>[] {
                Tuple.Create("1.2.3", 3),
                Tuple.Create(" 1.2.3 ", 3),
                Tuple.Create(" 2.2.3-4 ", 3),
                Tuple.Create(" 3.2.3-pre ", 3),
                Tuple.Create("v5.2.3", 3),
                Tuple.Create(" v8.2.3 ", 3),
                Tuple.Create("\t13.2.3", 3),
                Tuple.Create("=21.2.3", 3),
                Tuple.Create("v=34.2.3", 3),
            };

            foreach (var testCase in testCases)
            {
                var version = new SemVer.Version(testCase.Item1);
                Assert.Equal(version.Patch, testCase.Item2);
            }
        }
    }
}


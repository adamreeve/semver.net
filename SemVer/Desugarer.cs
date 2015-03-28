using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SemVer
{
    public static class Desugarer
    {
        // Allows patch-level changes if a minor version is specified
        // on the comparator. Allows minor-level changes if not.
        public static IEnumerable<Comparator> TildeRange(string spec)
        {
            const string pattern = @"~(.*)";

            var regex = new Regex(pattern);
            var match = regex.Match(spec);
            if (!match.Success)
            {
                return null;
            }

            Version minVersion = null;
            Version maxVersion = null;

            var version = new PartialVersion(match.Groups[1].Value);
            if (version.Minor.HasValue)
            {
                // Doesn't matter whether patch version is null or not,
                // the logic is the same, min patch version will be zero if null.
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major, version.Minor.Value + 1, 0);
            }
            else
            {
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major + 1, 0, 0);
            }
            var minComparator = new Comparator(
                    Comparator.Operator.GreaterThanOrEqual,
                    minVersion);
            var maxComparator = new Comparator(
                    Comparator.Operator.LessThan,
                    maxVersion);
            return new [] { minComparator, maxComparator };
        }

        public static IEnumerable<Comparator> CaretRange(string spec)
        {
            return null;
        }

        public static IEnumerable<Comparator> HyphenRange(string spec)
        {
            return null;
        }

        public static IEnumerable<Comparator> StarRange(string spec)
        {
            return null;
        }
    }
}

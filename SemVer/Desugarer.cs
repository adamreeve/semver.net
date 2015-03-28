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
            const string pattern = @"^~(.+)$";

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
                maxVersion = new Version(version.Major.Value, version.Minor.Value + 1, 0);
            }
            else
            {
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            return minMaxComparators(minVersion, maxVersion);
        }

        // Allows changes that do not modify the left-most non-zero digit
        // in the [major, minor, patch] tuple.
        public static IEnumerable<Comparator> CaretRange(string spec)
        {
            const string pattern = @"^\^(.+)$";

            var regex = new Regex(pattern);
            var match = regex.Match(spec);
            if (!match.Success)
            {
                return null;
            }

            Version minVersion = null;
            Version maxVersion = null;

            var version = new PartialVersion(match.Groups[1].Value);

            if (version.Major.Value > 0)
            {
                // Don't allow major version change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            else if (!version.Minor.HasValue)
            {
                // Don't allow major version change, even if it's zero
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(version.Major.Value + 1, 0, 0);
            }
            else if (!version.Patch.HasValue)
            {
                // Don't allow minor version change, even if it's zero
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, version.Minor.Value + 1, 0);
            }
            else if (version.Minor > 0)
            {
                // Don't allow minor version change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, version.Minor.Value + 1, 0);
            }
            else
            {
                // Only patch non-zero, don't allow patch change
                minVersion = version.ToZeroVersion();
                maxVersion = new Version(0, 0, version.Patch.Value + 1);
            }

            return minMaxComparators(minVersion, maxVersion);
        }

        public static IEnumerable<Comparator> HyphenRange(string spec)
        {
            return null;
        }

        public static IEnumerable<Comparator> StarRange(string spec)
        {
            return null;
        }

        private static Comparator[] minMaxComparators(Version minVersion, Version maxVersion)
        {
            var minComparator = new Comparator(
                    Comparator.Operator.GreaterThanOrEqual,
                    minVersion);
            var maxComparator = new Comparator(
                    Comparator.Operator.LessThan,
                    maxVersion);
            return new [] { minComparator, maxComparator };
        }
    }
}

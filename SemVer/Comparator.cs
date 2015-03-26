using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SemVer
{
    public class Comparator
    {
        private readonly ComparatorType _comparatorType;

        private readonly Version _comparatorVersion;

        private const string Pattern = @"^
            \s*
            ([=<>]*)  # Comparator type (can be empty)
            (\d.*)    # Version
            \s*
            $";

        public Comparator(string input)
        {
            var regex = new Regex(Pattern, RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid comparator string: {0}", input));
            }

            _comparatorType = ParseComparatorType(match.Groups[1].Value);
            _comparatorVersion = new Version(match.Groups[2].Value);
        }

        private static ComparatorType ParseComparatorType(string input)
        {
            switch (input)
            {
                case (""):
                case ("="):
                    return ComparatorType.Equal;
                case ("<"):
                    return ComparatorType.LessThan;
                case ("<="):
                    return ComparatorType.LessThanOrEqual;
                case (">"):
                    return ComparatorType.GreaterThan;
                case (">="):
                    return ComparatorType.GreaterThanOrEqual;
                default:
                    throw new ArgumentException(String.Format("Invalid comparator type: {0}", input));
            }
        }

        public bool Match(Version version)
        {
            switch(_comparatorType)
            {
                case(ComparatorType.Equal):
                    return version == _comparatorVersion;
                case(ComparatorType.LessThan):
                    return version < _comparatorVersion;
                case(ComparatorType.LessThanOrEqual):
                    return version <= _comparatorVersion;
                case(ComparatorType.GreaterThan):
                    return version > _comparatorVersion;
                case(ComparatorType.GreaterThanOrEqual):
                    return version >= _comparatorVersion;
                default:
                    throw new InvalidOperationException("Comparator type not recognised.");
            }
        }

        private enum ComparatorType
        {
            Equal = 0,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual,
        }
    }
}

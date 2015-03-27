using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SemVer
{
    public class Comparator : IEquatable<Comparator>
    {
        private readonly Operator _comparatorType;

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

        public Comparator(Operator comparatorType, Version comparatorVersion)
        {
            if (comparatorVersion == null)
            {
                throw new NullReferenceException("Null comparator version");
            }
            _comparatorType = comparatorType;
            _comparatorVersion = comparatorVersion;
        }

        private static Operator ParseComparatorType(string input)
        {
            switch (input)
            {
                case (""):
                case ("="):
                    return Operator.Equal;
                case ("<"):
                    return Operator.LessThan;
                case ("<="):
                    return Operator.LessThanOrEqual;
                case (">"):
                    return Operator.GreaterThan;
                case (">="):
                    return Operator.GreaterThanOrEqual;
                default:
                    throw new ArgumentException(String.Format("Invalid comparator type: {0}", input));
            }
        }

        public bool Match(Version version)
        {
            switch(_comparatorType)
            {
                case(Operator.Equal):
                    return version == _comparatorVersion;
                case(Operator.LessThan):
                    return version < _comparatorVersion;
                case(Operator.LessThanOrEqual):
                    return version <= _comparatorVersion;
                case(Operator.GreaterThan):
                    return version > _comparatorVersion;
                case(Operator.GreaterThanOrEqual):
                    return version >= _comparatorVersion;
                default:
                    throw new InvalidOperationException("Comparator type not recognised.");
            }
        }

        public enum Operator
        {
            Equal = 0,
            LessThan,
            LessThanOrEqual,
            GreaterThan,
            GreaterThanOrEqual,
        }

        public override string ToString()
        {
            string operatorString = null;
            switch(_comparatorType)
            {
                case(Operator.Equal):
                    operatorString = "=";
                    break;
                case(Operator.LessThan):
                    operatorString = "<";
                    break;
                case(Operator.LessThanOrEqual):
                    operatorString = "<=";
                    break;
                case(Operator.GreaterThan):
                    operatorString = ">";
                    break;
                case(Operator.GreaterThanOrEqual):
                    operatorString = ">=";
                    break;
                default:
                    throw new InvalidOperationException("Comparator type not recognised.");
            }
            return String.Format("{0}{1}", operatorString, _comparatorVersion);
        }

        public bool Equals(Comparator other)
        {
            if (other == null)
            {
                return false;
            }
            return ToString() == other.ToString();
        }

        public override bool Equals(object other)
        {
            return Equals(other as Comparator);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}

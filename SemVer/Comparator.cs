using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SemVer
{
    internal class Comparator : IEquatable<Comparator>
    {
        public readonly Operator ComparatorType;

        public readonly Version Version;

        private static Regex regex = new Regex(@"^
            \s*
            ([=<>]*)  # Comparator type (can be empty)
            (\d.*)    # Version
            \s*
            $",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public Comparator(string input)
        {
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid comparator string: {0}", input));
            }

            ComparatorType = ParseComparatorType(match.Groups[1].Value);
            Version = new Version(match.Groups[2].Value);
        }

        public Comparator(Operator comparatorType, Version comparatorVersion)
        {
            if (comparatorVersion == null)
            {
                throw new NullReferenceException("Null comparator version");
            }
            ComparatorType = comparatorType;
            Version = comparatorVersion;
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

        public bool IsSatisfied(Version version)
        {
            switch(ComparatorType)
            {
                case(Operator.Equal):
                    return version == Version;
                case(Operator.LessThan):
                    return version < Version;
                case(Operator.LessThanOrEqual):
                    return version <= Version;
                case(Operator.GreaterThan):
                    return version > Version;
                case(Operator.GreaterThanOrEqual):
                    return version >= Version;
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
            switch(ComparatorType)
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
            return String.Format("{0}{1}", operatorString, Version);
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

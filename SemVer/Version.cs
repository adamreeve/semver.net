using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SemVer
{
    public class Version : IComparable<Version>, IEquatable<Version>
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int Patch { get; set; }

        public Version(string input)
        {
            string pattern = @"^
                [v=\s]*
                (\d+)                    # major version
                \.
                (\d+)                    # minor version
                \.
                (\d+)                    # patch version
                (\-([0-9A-Za-z\-\.]+))?  # pre-release version
                (\+([0-9A-Za-z\-\.]+))?  # build metadata
                \s*
                $";

            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid version string: {0}", input));
            }

            Major = Int32.Parse(match.Groups[1].Value);

            Minor = Int32.Parse(match.Groups[2].Value);

            Patch = Int32.Parse(match.Groups[3].Value);
        }

        public Version(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", Major, Minor, Patch);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        // Implement IEquatable<Version>
        public bool Equals(Version other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            return PartComparisons(other).All(c => c == 0);
        }

        // Implement IComparable<Version>
        public int CompareTo(Version other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            foreach (var c in PartComparisons(other))
            {
                if (c != 0)
                {
                    return c;
                }
            }
            return 0;

        }

        private IEnumerable<int> PartComparisons(Version other)
        {
            yield return Major.CompareTo(other.Major);
            yield return Minor.CompareTo(other.Minor);
            yield return Patch.CompareTo(other.Patch);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Version);
        }

        public static bool operator ==(Version a, Version b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }
            return a.Equals(b);
        }

        public static bool operator !=(Version a, Version b)
        {
            return !(a == b);
        }

        public static bool operator >(Version a, Version b)
        {
            if (ReferenceEquals(a, null))
            {
                return false;
            }
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(Version a, Version b)
        {
            if (ReferenceEquals(a, null))
            {
                return false;
            }
            return a.CompareTo(b) >= 0;
        }

        public static bool operator <(Version a, Version b)
        {
            if (ReferenceEquals(a, null))
            {
                return false;
            }
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(Version a, Version b)
        {
            if (ReferenceEquals(a, null))
            {
                return false;
            }
            return a.CompareTo(b) <= 0;
        }
    }
}

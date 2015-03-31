using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SemVer
{
    public class Version : IComparable<Version>, IEquatable<Version>
    {
        private readonly string _inputString;
        private readonly int _major;
        private readonly int _minor;
        private readonly int _patch;
        private readonly string _preRelease;
        private readonly string _build;

        public int Major { get { return _major; } }

        public int Minor { get { return _minor; } }

        public int Patch { get { return _patch; } }

        public string PreRelease { get { return _preRelease; } }

        public string Build { get {return _build; } }

        private static Regex regex = new Regex(@"^
            [v=\s]*
            (\d+)                    # major version
            \.
            (\d+)                    # minor version
            \.
            (\d+)                    # patch version
            (\-([0-9A-Za-z\-\.]+))?  # pre-release version
            (\+([0-9A-Za-z\-\.]+))?  # build metadata
            \s*
            $",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public Version(string input)
        {
            _inputString = input;

            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid version string: {0}", input));
            }

            _major = Int32.Parse(match.Groups[1].Value);

            _minor = Int32.Parse(match.Groups[2].Value);

            _patch = Int32.Parse(match.Groups[3].Value);

            if (match.Groups[4].Success)
            {
                _preRelease = match.Groups[4].Value;
            }

            if (match.Groups[5].Success)
            {
                _build = match.Groups[5].Value;
            }
        }

        public Version(int major, int minor, int patch)
        {
            _major = major;
            _minor = minor;
            _patch = patch;
        }

        public Version BaseVersion()
        {
            return new Version(Major, Minor, Patch);
        }

        public override string ToString()
        {
            return _inputString ?? Clean();
        }

        public string Clean()
        {
            var preReleaseString = PreRelease == null ? ""
                : String.Format("-{0}", PreRelease);
            var buildString = Build == null ? ""
                : String.Format("+{0}", Build);

            return String.Format("{0}.{1}.{2}{3}{4}",
                    Major, Minor, Patch, preReleaseString, buildString);
        }

        public override int GetHashCode()
        {
            return Clean().GetHashCode();
        }

        // Implement IEquatable<Version>
        public bool Equals(Version other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            return CompareTo(other) == 0;
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

            return PreReleaseVersion.Compare(this.PreRelease, other.PreRelease);
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

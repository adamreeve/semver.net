using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SemVer
{
    // A version that might not have a minor or patch
    // number, for use in ranges like "^1.2" or "2.x"
    internal class PartialVersion
    {
        public int? Major { get; set; }

        public int? Minor { get; set; }

        public int? Patch { get; set; }

        public PartialVersion(string input)
        {
            const string pattern = @"^
                (\d+|[Xx\*])                     # major version
                (
                    \.
                    (\d+|[Xx\*])                 # minor version
                    (
                        \.
                        (\d+|[Xx\*])             # patch version
                        (\-([0-9A-Za-z\-\.]+))?  # pre-release version
                        (\+([0-9A-Za-z\-\.]+))?  # build metadata
                    )?
                )?
                $";

            string[] xValues = { "X", "x", "*" };

            if (input.Trim() == "")
            {
                // Empty input means any version
                return;
            }

            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid version string: {0}", input));
            }

            if (xValues.Contains(match.Groups[1].Value))
            {
                Major = null;
            }
            else
            {
                Major = Int32.Parse(match.Groups[1].Value);
            }

            if (match.Groups[2].Success)
            {
                if (xValues.Contains(match.Groups[3].Value))
                {
                    Minor = null;
                }
                else
                {
                    Minor = Int32.Parse(match.Groups[3].Value);
                }
            }

            if (match.Groups[4].Success)
            {
                if (xValues.Contains(match.Groups[5].Value))
                {
                    Patch = null;
                }
                else
                {
                    Patch = Int32.Parse(match.Groups[5].Value);
                }
            }
        }

        public Version ToZeroVersion()
        {
            return new Version(
                    Major ?? 0,
                    Minor ?? 0,
                    Patch ?? 0);
        }
    }
}

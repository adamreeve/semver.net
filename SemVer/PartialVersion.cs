using System;
using System.Text.RegularExpressions;

namespace SemVer
{
    // A version that might not have a minor or patch
    // number, for use in ranges like "^1.2"
    internal class PartialVersion
    {
        public int Major { get; set; }

        public int? Minor { get; set; }

        public int? Patch { get; set; }

        public PartialVersion(string input)
        {
            string pattern = @"^
                (\d+)                            # major version
                (
                    \.
                    (\d+)                        # minor version
                    (
                        \.
                        (\d+)                    # patch version
                        (\-([0-9A-Za-z\-\.]+))?  # pre-release version
                        (\+([0-9A-Za-z\-\.]+))?  # build metadata
                    )?
                )?
                $";

            var regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var match = regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException(String.Format("Invalid version string: {0}", input));
            }

            Major = Int32.Parse(match.Groups[1].Value);

            if (match.Groups[2].Success)
            {
                Minor = Int32.Parse(match.Groups[3].Value);
            }

            if (match.Groups[4].Success)
            {
                Patch = Int32.Parse(match.Groups[5].Value);
            }
        }

        public Version ToZeroVersion()
        {
            return new Version(
                    Major,
                    Minor ?? 0,
                    Patch ?? 0);
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace SemVer
{
    public class Version
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
                throw new ArgumentException("Invalid version string");
            }

            Major = Int32.Parse(match.Groups[1].Value);

            Minor = Int32.Parse(match.Groups[2].Value);

            Patch = Int32.Parse(match.Groups[3].Value);
        }
    }
}


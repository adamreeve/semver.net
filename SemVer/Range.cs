using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    public class Range
    {
        private readonly IEnumerable<ComparatorSet> _comparatorSets;

        public Range(string rangeSpec)
        {
            var comparatorSetSpecs = rangeSpec.Split(new [] {"||"}, StringSplitOptions.None);
            _comparatorSets = comparatorSetSpecs.Select(s => new ComparatorSet(s));
        }

        public bool Match(Version version)
        {
            return _comparatorSets.Any(s => s.Match(version));
        }

        public bool Match(string versionString)
        {
            var version = new Version(versionString);
            return Match(version);
        }

        public IEnumerable<Version> Filter(IEnumerable<Version> versions)
        {
            return versions.Where(Match);
        }

        public IEnumerable<string> Filter(IEnumerable<string> versions)
        {
            return versions.Where(Match);
        }

        public Version Select(IEnumerable<Version> versions)
        {
            return Filter(versions).Max();
        }

        public string Select(IEnumerable<string> versionStrings)
        {
            var versions = versionStrings.Select(s => new Version(s));
            var maxVersion = Select(versions);
            return maxVersion == null ? null : maxVersion.ToString();
        }
    }
}
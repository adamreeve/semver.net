using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    public class Range
    {
        private readonly IEnumerable<ComparatorSet> _comparatorSets;

        public Range(string rangeSpec, bool loose=false)
        {
            var comparatorSetSpecs = rangeSpec.Split(new [] {"||"}, StringSplitOptions.None);
            _comparatorSets = comparatorSetSpecs.Select(s => new ComparatorSet(s));
        }

        public bool IsSatisfied(Version version)
        {
            return _comparatorSets.Any(s => s.IsSatisfied(version));
        }

        public bool IsSatisfied(string versionString, bool loose=false)
        {
            var version = new Version(versionString);
            return IsSatisfied(version);
        }

        public IEnumerable<Version> Satisfying(IEnumerable<Version> versions)
        {
            return versions.Where(IsSatisfied);
        }

        public IEnumerable<string> Satisfying(IEnumerable<string> versions, bool loose=false)
        {
            return versions.Where(v => IsSatisfied(v, loose));
        }

        public Version MaxSatisfying(IEnumerable<Version> versions)
        {
            return Satisfying(versions).Max();
        }

        public string MaxSatisfying(IEnumerable<string> versionStrings, bool loose=false)
        {
            var versions = ValidVersions(versionStrings, loose);
            var maxVersion = MaxSatisfying(versions);
            return maxVersion == null ? null : maxVersion.ToString();
        }

        // Static convenience methods

        public static bool IsSatisfied(string rangeSpec, string versionString, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.IsSatisfied(versionString);
        }

        public static IEnumerable<string> Satisfying(string rangeSpec, IEnumerable<string> versions, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.Satisfying(versions);
        }

        public static string MaxSatisfying(string rangeSpec, IEnumerable<string> versionStrings, bool loose=false)
        {
            var range = new Range(rangeSpec);
            return range.MaxSatisfying(versionStrings);
        }

        private IEnumerable<Version> ValidVersions(IEnumerable<string> versionStrings, bool loose)
        {
            foreach (var v in versionStrings)
            {
                Version version = null;

                try
                {
                    version = new Version(v, loose);
                }
                catch (ArgumentException) { } // Skip

                if (version != null)
                {
                    yield return version;
                }
            }
        }
    }
}

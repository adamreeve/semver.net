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
            var comparatorSetSpecs = rangeSpec.Split(new [] {"||"}, StringSplitOptions.RemoveEmptyEntries);
            _comparatorSets = comparatorSetSpecs.Select(s => new ComparatorSet(s));
        }

        public bool Match(Version version)
        {
            return _comparatorSets.Any(s => s.Match(version));
        }

        public IEnumerable<Version> Filter(IEnumerable<Version> versions)
        {
            return versions.Where(Match);
        }

        public Version Select(IEnumerable<Version> versions)
        {
            return Filter(versions).Max();
        }
    }
}
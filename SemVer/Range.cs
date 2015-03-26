using System;
using System.Collections.Generic;

namespace SemVer
{
    public class Range
    {
        private IEnumerable<ComparatorSet> ComparatorSets;

        public Range(string spec)
        {
        }

        public bool Match(Version version)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Version> Filter(IEnumerable<Version> versions)
        {
            throw new NotImplementedException();
        }

        public Version Select(IEnumerable<Version> versions)
        {
            throw new NotImplementedException();
        }
    }
}
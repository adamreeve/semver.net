using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    public class ComparatorSet
    {
        private readonly IEnumerable<Comparator> _comparators;

        public ComparatorSet(string spec)
        {
            var comparatorSpecs = spec.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            _comparators = comparatorSpecs.Select(s => new Comparator(s));
        }

        public bool Match(Version version)
        {
            return _comparators.All(c => c.Match(version));
        }
    }
}

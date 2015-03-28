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
            spec = spec.Trim();
            // A comparator set might be an advanced range specifier
            // like ~1.2.3, ^1.2, or 1.*.
            // Check for these first before standard comparator sets:
            foreach (var desugarer in new Func<string, IEnumerable<Comparator>>[] {
                    Desugarer.TildeRange,
                    Desugarer.CaretRange,
                    Desugarer.HyphenRange,
                    Desugarer.StarRange,
                    })
            {
                _comparators = desugarer(spec);
                if (_comparators != null)
                {
                    break;
                }
            }
            if (_comparators == null)
            {
                // Standard set of whitespace separated comparators:
                var comparatorSpecs = spec.Split(
                        (char[])null, StringSplitOptions.RemoveEmptyEntries);
                _comparators = comparatorSpecs.Select(s => new Comparator(s));
            }
        }

        public bool Match(Version version)
        {
            return _comparators.All(c => c.Match(version));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    internal class ComparatorSet : IEquatable<ComparatorSet>
    {
        private readonly List<Comparator> _comparators;

        public ComparatorSet(string spec)
        {
            _comparators = new List<Comparator> {};

            spec = spec.Trim();
            if (spec == "")
            {
                spec = "*";
            }

            int position = 0;
            int end = spec.Length;

            while (position < end)
            {
                int iterStartPosition = position;

                // A comparator set might be an advanced range specifier
                // like ~1.2.3, ^1.2, or 1.*.
                // Check for these first before standard comparators:
                foreach (var desugarer in new Func<string, Tuple<int, Comparator[]>>[] {
                        Desugarer.HyphenRange,
                        Desugarer.TildeRange,
                        Desugarer.CaretRange,
                        Desugarer.StarRange,
                        })
                {
                    var result = desugarer(spec.Substring(position));
                    if (result != null)
                    {
                        position += result.Item1;
                        _comparators.AddRange(result.Item2);
                    }
                }

                // Check for standard comparator with operator and version:
                var comparatorResult = Comparator.TryParse(spec.Substring(position));
                if (comparatorResult != null)
                {
                    position += comparatorResult.Item1;
                    _comparators.Add(comparatorResult.Item2);
                }

                if (position == iterStartPosition)
                {
                    // Didn't manage to read any valid comparators
                    throw new ArgumentException(String.Format("Invalid range specification: \"{0}\"", spec));
                }
            }
        }

        public bool IsSatisfied(Version version)
        {
            bool satisfied = _comparators.All(c => c.IsSatisfied(version));
            if (version.PreRelease != null)
            {
                // If the version is a pre-release, then one of the
                // comparators must have the same version and also include
                // a pre-release tag.
                return satisfied && _comparators.Any(c =>
                        c.Version.PreRelease != null &&
                        c.Version.BaseVersion() == version.BaseVersion());
            }
            else
            {
                return satisfied;
            }
        }

        public bool Equals(ComparatorSet other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            var thisSet = new HashSet<Comparator>(_comparators);
            return thisSet.SetEquals(other._comparators);
        }

        public override bool Equals(object other)
        {
            return Equals(other as ComparatorSet);
        }

        public override int GetHashCode()
        {
            // XOR is commutative, so this hash code is independent
            // of the order of comparators.
            return _comparators.Aggregate(0, (accum, next) => accum ^ next.GetHashCode());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace SemVer
{
    static internal class PreReleaseVersion
    {
        public static int Compare(string a, string b)
        {
            if (a == null && b == null)
            {
                return 0;
            }
            else if (a == null)
            {
                // No pre-release is > having a pre-release version
                return 1;
            }
            else if (b == null)
            {
                return -1;
            }
            else
            {
                foreach (var c in IdentifierComparisons(a.Split('.'), b.Split('.')))
                {
                    if (c != 0)
                    {
                        return c;
                    }
                }
                return 0;
            }
        }

        private static IEnumerable<int> IdentifierComparisons(
                IEnumerable<string> aIdentifiers, IEnumerable<string> bIdentifiers)
        {
            foreach (var identifiers in ZipIdentifiers(aIdentifiers, bIdentifiers))
            {
                var a = identifiers.Item1;
                var b = identifiers.Item2;
                if (a == b)
                {
                    yield return 0;
                }
                else if (a == null)
                {
                    yield return -1;
                }
                else if (b == null)
                {
                    yield return 1;
                }
                else
                {
                    bool aIsNumeric = IsNumeric(a);
                    bool bIsNumeric = IsNumeric(b);
                    if (aIsNumeric && bIsNumeric)
                    {
                        yield return Int32.Parse(a).CompareTo(Int32.Parse(b));
                    }
                    else if (!aIsNumeric && !bIsNumeric)
                    {
                        yield return String.CompareOrdinal(a, b);
                    }
                    else if (aIsNumeric && !bIsNumeric)
                    {
                        yield return -1;
                    }
                    else // !aIsNumeric && bIsNumeric
                    {
                        yield return 1;
                    }
                }
            }
        }

        private static bool IsNumeric(string identifier)
        {
            if (identifier.StartsWith("0"))
            {
                return false;
            }
            int x;
            bool couldParse = Int32.TryParse(identifier, out x);
            return couldParse && x >= 0;
        }

        // Zip identifier sets until both have been exhausted, returning null
        // for identifier components not in one set.
        private static IEnumerable<Tuple<string, string>> ZipIdentifiers(
                IEnumerable<string> first, IEnumerable<string> second)
        {
            using (var ie1 = first.GetEnumerator())
            using (var ie2 = second.GetEnumerator())
            {
                while (ie1.MoveNext())
                {
                    if (ie2.MoveNext())
                    {
                        yield return Tuple.Create(ie1.Current, ie2.Current);
                    }
                    else
                    {
                        yield return Tuple.Create<string, string>(ie1.Current, null);
                    }
                }
                while (ie2.MoveNext())
                {
                    yield return Tuple.Create<string, string>(null, ie2.Current);
                }
            }
        }
    }
}

SemVer.NET: The semantic versioner for .NET.
============================================

[![Build Status](https://travis-ci.org/adamreeve/semver.net.svg?branch=master)](https://travis-ci.org/adamreeve/semver.net)

This library implements the
[Semantic Versioning specification](http://semver.org/)
and the version range specification used by
npm ([node-semver](https://github.com/npm/node-semver)).

Installation
------------

SemVer.NET is available as a [NuGet package](https://www.nuget.org/packages/SemanticVersioning/).
To install it, run the following command in the Package Manager Console:

```
Install-Package SemanticVersioning
```

Quick Start
-----------

Construct a range:

```C#
var range = new SemVer.Range("~1.2.3")
```

Construct a version:
```C#
var version = new SemVer.Version("1.2.4")
```

Test whether the version satisfies the range:
```C#
bool satisfied = range.IsSatisfied(version)
// satisfied = true
```

Ranges
------

SemVer.NET range specifications match the range specifications
used by [node-semver](https://github.com/npm/node-semver).

A range specification is constructed by combining multiple
comparator sets with `||`, where the range is satisfied if any
of the comparator sets are satisfied.

A comparator set is a combination of comparators, where all comparators
must be satisfied for a comparator set to be satisfied.

A comparator is made up of an operator and a version.
An operator is one of: `==`, `>`, `>=`, `<`, or `<=`.
For example, the comparator `>=1.2.3` specifies versions
greater than or equal to `1.2.3`.

An example of a range is `>=1.2.3 <1.3.0 || ==1.3.2`, which
is satisfied by `1.2.3`, `1.2.99`, and `1.3.2`, but not `1.3.0`.

Advanced Ranges
---------------

Ranges can also be specified using advanced range specification strings, which
desugar into comparator sets.

### Hyphen Ranges

A hyphen range specifies an inclusive range of valid versions, for example
`1.2.3 - 1.4.2`
is equivalent to `>=1.2.3 <=1.4.2`.

### X-Ranges

A partial version string, or a version string with components replaced by an `X` or a `*`
matches any version where the specified components match.

For example, `1.2.x` is satisfied by `1.2.0` and `1.2.99`, but not `1.3.0`.

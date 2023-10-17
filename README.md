Semantic Versioning for .NET
============================

[![CI Status](https://github.com/adamreeve/semver.net/actions/workflows/ci.yml/badge.svg?branch=master&event=push)](https://github.com/adamreeve/semver.net/actions/workflows/ci.yml?query=branch%3Amaster+event%3Apush)
[![NuGet Version](https://img.shields.io/nuget/v/SemanticVersioning.svg)](https://www.nuget.org/packages/SemanticVersioning/)

This library implements the
[Semantic Versioning specification](http://semver.org/)
and the version range specifications used by
npm ([node-semver](https://github.com/npm/node-semver)).

Installation
------------

SemanticVersioning is available as a [NuGet package](https://www.nuget.org/packages/SemanticVersioning/).
To install it, run the following command in the Package Manager Console:

```
Install-Package SemanticVersioning
```

Quick Start
-----------

Use the `SemanticVersioning` namespace:
```
using SemanticVersioning;
```

Construct a range:

```C#
var range = new Range("~1.2.3");
```

Construct a version:
```C#
var version = new Version("1.2.4");
```

Test whether the version satisfies the range:
```C#
bool satisfied = range.IsSatisfied(version);
// satisfied = true
```

Filter a list of versions to select only those that
satisfy a range:
```C#
var versions = new [] {
    new Version("1.2.1"),
    new Version("1.2.3"),
    new Version("1.2.8"),
    new Version("1.3.2"),
};
IEnumerable<Version> satisfyingVersions = range.Satisfying(versions);
// satisfyingVersions = 1.2.3, 1.2.8
```

Find the maximum version that satisfies a range:
```C#
Version selectedVersion = range.MaxSatisfying(versions);
// selectedVersion = 1.2.8
```

To get the original input string used when constructing a
version, use `Version.ToString()`.

Ranges
------

SemanticVersioning range specifications match the range specifications
used by [node-semver](https://github.com/npm/node-semver).

A range specification is constructed by combining multiple
comparator sets with `||`, where the range is satisfied if any
of the comparator sets are satisfied.

A comparator set is a combination of comparators, where all comparators
must be satisfied for a comparator set to be satisfied.

A comparator is made up of an operator and a version.
An operator is one of: `=`, `>`, `>=`, `<`, or `<=`.
For example, the comparator `>=1.2.3` specifies versions
greater than or equal to `1.2.3`.

An example of a range is `>=1.2.3 <1.3.0 || =1.3.2`, which
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

### Tilde Ranges

When a minor version is specified, a tilde range only allows changes in the
patch version. Otherwise if only the major version is specified, only
changes in the minor version are allowed.

Examples:

* `~1.2.3` is equivalent to `>=1.2.3 < 1.3.0`
* `~1.2` is equivalent to `>=1.2.0 < 1.3.0`
* `~1` is equivalent to `>=1.0.0 < 2.0.0`

### Caret Ranges

A caret range allows versions where the most significant non-zero
version component does not change.

Examples:

* `^1.2.3` is equivalent to `>=1.2.3 < 2.0.0`
* `^0.2.3` is equivalent to `>=0.2.3 < 0.3.0`
* `^0.0.3` is equivalent to `>=0.0.3 < 0.0.4`

Pre-Release Versions
--------------------

Versions with a pre-release can normally only satisfy ranges that contain
a comparator with a pre-release version, and the comparator
version's major, minor and patch components must match those of the
version being tested.

```C#
var range = new Range(">=1.2.3-beta.2");
range.IsSatisfied("1.2.3-beta.3");  // true
range.IsSatisfied("1.2.3-alpha");   // false
range.IsSatisfied("1.2.3");         // true
range.IsSatisfied("1.2.4");         // true
range.IsSatisfied("1.2.4-beta.5");  // false

var range2 = new Range(">=1.2.3");
range2.IsSatisfied("1.2.4-alpha");  // false
```

To change this behaviour and allow any pre-release version to satisfy a range,
you can set the `includePrerelease` argument to true:

```C#
var range = new Range(">=1.2.3-beta.2");
range.IsSatisfied("1.2.4-beta.5", includePrerelease=true);  // true

var range2 = new Range(">=1.2.3");
range2.IsSatisfied("1.2.4-alpha", includePrerelease=true);  // true
```

The `Range.Satisfying` and `Range.MaxSatisfying` methods similarly support
an `includePrerelease` argument to allow any pre-release version.

Version Comparisons
-------------------

`Version` objects implement [`IEquatable<Version>`](https://msdn.microsoft.com/en-us/library/ms131187%28v=vs.110%29.aspx)
and [`IComparable<Version>`](https://msdn.microsoft.com/en-us/library/4d7sx9hd%28v=vs.110%29.aspx), and
can also be compared using `==`, `>`, `>=`, `<`, `<=` and `!=`.

```C#
var a = new Version("1.2.3");
var b = new Version("1.3.0");
a == b;  // false
a != b;  // true
a > b;   // false
a < b;   // true
a <= b;  // true
```


Usage Notes
-----------

The `Range` and `Version` constructors will throw
an `ArgumentException` when an invalid range or version
string is used.
These constructors and all methods that accept versions
as a string have an optional `loose` parameter, which will
allow some invalid version formats. For example, a pre-release
version without a leading hyphen
will be allowed when `loose = true`.

```C#
var version = new Version("1.2.3alpha");       // Throws ArgumentException
var version = new Version("1.2.3alpha", true); // No exception thrown
```

The `Range` class contains separate methods that accept
versions either as strings or as `Version` objects.
When passing versions as a string to range methods,
invalid version strings will act as if the version does
not satisfy the range, but no exception will be thrown.
Therefore, if you want to know when a version string is invalid,
you should construct `Version` objects and check for an `ArgumentException`.

```C#
var range = new Range("~1.2.3");
// Returns false:
range.IsSatisfied("banana");
// Version constructor throws ArgumentException:
range.IsSatisfied(new Version("banana"));
```

For convenience, static methods of the `Range` class are provided
that accept a range parameter as the first argument and accept versions
as strings, so you don't have to
construct any `Range` or `Version` objects if you just want to use one method:

```C#
// Returns 1.2.8:
Range.MaxSatisfying("~1.2.3",
        new [] {"1.2.1", "1.2.3", "1.2.8", "1.3.2"});
```

SemVer.NET: The semantic versioner for .NET.
============================================

This library aims to implements the
[Semantic Versioning specification](http://semver.org/)
and the version range specification used by
npm ([node-semver](https://github.com/npm/node-semver)).

Here's what currently works and what is still to do:

- [x] Parsing basic version strings with major, minor and patch numbers.
- [x] Implement equality and comparison operators for versions.
- [x] Implement comparator strings, eg >=1.2.3, <1.3
- [x] Implement simple ranges consisting of comparator sets.
- [ ] Implement desugaring of tilde, caret and star ranges.
- [ ] Add support for prerelease and build versions.
- [ ] Write docs.

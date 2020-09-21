using Xunit;

namespace SemVer.Tests
{
    public class TryParse
    {
        [Theory]
        [InlineData("1.0.0", false, true)]
        [InlineData("v 1.2.3", true, true)]
        [InlineData("Not SemVer", false, false)]
        public void VersionTryParse(string input, bool loose, bool expectedResult)
        {
            //Given

            //When
            var success = Version.TryParse(input, loose, out var version);

            //Then
            Assert.Equal(expectedResult, success);
            if (success)
            {
                Assert.NotNull(version);
            }
            else
            {
                Assert.Null(version);
            }
        }


        [Theory]
        [InlineData("~1.0.0", false, true)]
        [InlineData("< 1", true, true)]
        [InlineData("Not SemVer Range", false, false)]
        public void RangeTryParse(string rangeSpec, bool loose, bool expectedResult)
        {
            //Given

            //When
            var success = Range.TryParse(rangeSpec, loose, out var range);

            //Then
            Assert.Equal(expectedResult, success);
            if (success)
            {
                Assert.NotNull(range);
            }
            else {
                Assert.Null(range);
            }
        }
    }
}
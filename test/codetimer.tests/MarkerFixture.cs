using CodeTimer.Abstractions;
using Xunit;

namespace CodeTimer.Tests
{

    public class MarkerFixture
    {

        [Theory]
        [InlineData(1000, "My Marker", "1000 - My Marker")]
        [InlineData(1000, null, "1000")]
        [InlineData(1000, "", "1000")]
        [InlineData(1000, "    ", "1000")]
        public void ShouldFormatToStringCorrectly(long ticks, string name, string expected)
        {
            // Arrange
            var marker = new Marker(ticks, name);
            // Act
            var actual = marker.ToString();

            // Assert
            Assert.True(actual.Equals(expected));
        }
    }
}

using Xunit;

namespace CodeTimer.Tests
{

    public class FormattingFixture
    {
        [Theory]
        [InlineData(null, "Step 1", 500, "Step 1 - 500")]
        [InlineData("MyMethod", "Step 1", 500, "Step 1 - 500")]
        [InlineData("MyMethod", null, 500, "500")]
        [InlineData(null, null, 500, "500")]
        public void ShouldFormatSingleMark(string timerName, string segmentName, long ticks, string expected)
        {
            // Arrange
            var timer = new CodeTimer(timerName);
            timer.Mark(segmentName);

            // Act
            timer.Complete();
            var result = timer.GetFormattedResult();

            // Assert
            Assert.True(result == expected, $"Result: {result}, Expected: {expected}");
        }
    }
}

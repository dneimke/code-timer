using Xunit;

namespace CodeTimer.Tests
{

    public class ElapsedFixture
    {
        [Theory]
        [InlineData(null, "Step 1", 500, 500)]
        [InlineData("MyMethod", "Step 1", 500, 500)]
        [InlineData("MyMethod", null, 500, 500)]
        [InlineData(null, null, 500, 500)]
        public void ShouldElapsedSingleMark(string timerName, string segmentName, long ticks, long expected)
        {
            // Arrange
            var timer = new CodeTimer(timerName);
            timer.Mark(segmentName);

            // Act
            timer.Complete();
            var result = timer.GetElapsedMilliseconds();

            // Assert
            Assert.True(result == expected, $"Result: {result}, Expected: {expected}");
        }
    }
}

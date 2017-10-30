
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CodeTimer.Abstractions;
using CodeTimer.Tests.Helpers;
using Xunit;

namespace CodeTimer.Tests 
{

    public class LogFormatterFixture
    {

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldFormatVerboseWhenMarkersPresent(ICodeTimer codeTimer, string expectedNonVerbose, string expectedVerbose)
        {
            // Arrange
            codeTimer.Verbose = true;

            // Act
            var actual = codeTimer.GetFormattedResult();

            // Assert
            Assert.True(actual.Equals(expectedVerbose), $"{Environment.NewLine}Expected: {expectedVerbose}{Environment.NewLine}Actual: {actual}");
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ShouldFormatNonVerboseWhenMarkersPresent(ICodeTimer codeTimer, string expectedNonVerbose, string expectedVerbose)
        {
            // Arrange
            
            // Act
            var actual = codeTimer.GetFormattedResult();

            // Assert
            Assert.True(actual.Equals(expectedNonVerbose), $"{Environment.NewLine}Expected: {expectedNonVerbose}{Environment.NewLine}Actual: {actual}");
        }


        [Fact]
        public void ShouldFormatWhenMarkersEmpty()
        {
            // Arrange

            // Act

            // Assert
        }


        [Fact]
        public void ShouldFormatWhenMarkersAreNull()
        {
            // Arrange

            // Act

            // Assert
        }
    }


    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            FailsWithMarksCase(),
            SucceedsWithNoExpectedCase(),
            SucceedsWithExpectedCase()
        };

        static object[] FailsWithMarksCase() 
        {
            var timer = new TestPerformanceTimer(0);
            var codeTimer = new CodeTimer(new CodeTimerOptions
            {
                Name = "Case1",
                PerformanceTimer = timer,
                ExpectedMilliseconds = 1000
            });

            timer.SetElapsedMilliseconds(400);
            codeTimer.Mark("Start");

            timer.SetElapsedMilliseconds(800);
            codeTimer.Mark("Middle");

            timer.SetElapsedMilliseconds(1200);
            codeTimer.Mark("End");

            var sb = new StringBuilder();
            sb.AppendLine("Case1 timer failed.  Ran for 1200ms, expected 1000ms.");
            sb.AppendLine(" - Start: 400ms");
            sb.AppendLine(" - Middle: 800ms");
            sb.AppendLine(" - End: 1200ms");
            
            var expectedVerbose = sb.ToString();
            var expectedNonVerbose = $"Case1,1000,1200,failed,400,800,1200";

            return new object[] {codeTimer, expectedNonVerbose, expectedVerbose};
        }

        static object[] SucceedsWithNoExpectedCase() 
        {
            var timer = new TestPerformanceTimer(0);
            var codeTimer = new CodeTimer(new CodeTimerOptions
            {
                Name = "Case2",
                PerformanceTimer = timer
            });

            timer.SetElapsedMilliseconds(400);
            codeTimer.Mark("Start");

            timer.SetElapsedMilliseconds(800);
            codeTimer.Mark("Middle");

            timer.SetElapsedMilliseconds(1200);
            codeTimer.Mark("End");

            var sb = new StringBuilder();
            sb.AppendLine("Case2 timer succeeded.  Ran for 1200ms.");
            sb.AppendLine(" - Start: 400ms");
            sb.AppendLine(" - Middle: 800ms");
            sb.AppendLine(" - End: 1200ms");

            var expectedVerbose = sb.ToString();
            var expectedNonVerbose = $"Case2,0,1200,succeeded,400,800,1200";

            return new object[] { codeTimer, expectedNonVerbose, expectedVerbose };
        }


        static object[] SucceedsWithExpectedCase() 
        {
            var timer = new TestPerformanceTimer(0);
            var codeTimer = new CodeTimer(new CodeTimerOptions
            {
                Name = "Case3",
                PerformanceTimer = timer,
                ExpectedMilliseconds = 1000
            });

            timer.SetElapsedMilliseconds(400);
            codeTimer.Mark("Start");

            timer.SetElapsedMilliseconds(800);
            codeTimer.Mark("Middle");

            timer.SetElapsedMilliseconds(1000);
            codeTimer.Mark("End");

            var sb = new StringBuilder();
            sb.AppendLine("Case3 timer succeeded.  Ran for 1000ms, expected 1000ms.");
            sb.AppendLine(" - Start: 400ms");
            sb.AppendLine(" - Middle: 800ms");
            sb.AppendLine(" - End: 1000ms");

            var expectedVerbose = sb.ToString();
            var expectedNonVerbose = $"Case3,1000,1000,succeeded,400,800,1000";

            return new object[] { codeTimer, expectedNonVerbose, expectedVerbose };
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return ((IEnumerable<object[]>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
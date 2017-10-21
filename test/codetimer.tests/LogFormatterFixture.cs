
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
        public void ShouldFormatWhenMarkersPresent(ICodeTimer codeTimer, string expected)
        {
            // Arrange

            // Act
            var actual = codeTimer.GetFormattedResult();

            // Assert
            Assert.True(actual.Equals(expected), $"{Environment.NewLine}Expected: {expected}{Environment.NewLine}Actual: {actual}");
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
            var codeTimer = new CodeTimer("Case1", null, timer)
            {
                ExpectedMilliseconds = 1000
            };

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
            
            var expected = sb.ToString();

            return new object[] {codeTimer, expected};
        }

        static object[] SucceedsWithNoExpectedCase() 
        {
            var timer = new TestPerformanceTimer(0);
            var codeTimer = new CodeTimer("Case1", null, timer);

            timer.SetElapsedMilliseconds(400);
            codeTimer.Mark("Start");

            timer.SetElapsedMilliseconds(800);
            codeTimer.Mark("Middle");

            timer.SetElapsedMilliseconds(1200);
            codeTimer.Mark("End");

            var sb = new StringBuilder();
            sb.AppendLine("Case1 timer succeeded.  Ran for 1200ms.");
            sb.AppendLine(" - Start: 400ms");
            sb.AppendLine(" - Middle: 800ms");
            sb.AppendLine(" - End: 1200ms");

            var expected = sb.ToString();

            return new object[] { codeTimer, expected };
        }


        static object[] SucceedsWithExpectedCase() 
        {
            var timer = new TestPerformanceTimer(0);
            var codeTimer = new CodeTimer("Case1", null, timer)
            {
                ExpectedMilliseconds = 1000
            };

            timer.SetElapsedMilliseconds(400);
            codeTimer.Mark("Start");

            timer.SetElapsedMilliseconds(800);
            codeTimer.Mark("Middle");

            timer.SetElapsedMilliseconds(1000);
            codeTimer.Mark("End");

            var sb = new StringBuilder();
            sb.AppendLine("Case1 timer succeeded.  Ran for 1000ms, expected 1000ms.");
            sb.AppendLine(" - Start: 400ms");
            sb.AppendLine(" - Middle: 800ms");
            sb.AppendLine(" - End: 1000ms");

            var expected = sb.ToString();

            return new object[] { codeTimer, expected };
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
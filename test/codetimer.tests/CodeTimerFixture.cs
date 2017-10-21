
using System.Collections;
using System.Collections.Generic;
using CodeTimer.Abstractions;
using CodeTimer.Tests.Helpers;
using Xunit;

namespace CodeTimer.Tests 
{

    public class CodeTimerFixture
    {

        [Theory]
        [ClassData(typeof(CodeTimerTestDataGenerator1))]
        public void ShouldReturnCorrectSuccessValue(ICodeTimer codeTimer, bool extected)
        {
            // Arrange

            // Act
            var actual = codeTimer.Success();

            // Assert
            Assert.True(actual == extected);
        }

        [Theory]
        [ClassData(typeof(CodeTimerTestDataGenerator2))]
        public void ShouldCreateMarkersWhenMarked(ICodeTimer codeTimer, int extected)
        {
            // Arrange

            // Act
            var actual = codeTimer.GetMarkers().Count;

            // Assert
            Assert.True(actual == extected);
        }

    }


    public class CodeTimerTestDataGenerator1 : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            ActualLessThanExpectedCase(),
            ActualEqualToExpectedCase(),
            ActualGreaterThanExpectedCase(),
            ExpectedNotSetCase()
        };

        static object[] ActualLessThanExpectedCase() 
        {
            var timer = new TestPerformanceTimer(900);
            var codeTimer = new CodeTimer("Case2", null, timer)
            {
                ExpectedMilliseconds = 1000
            };

            return new object[] { codeTimer, true };
        }

        static object[] ActualEqualToExpectedCase() 
        {
            var timer = new TestPerformanceTimer(1000);
            var codeTimer = new CodeTimer("Case2", null, timer)
            {
                ExpectedMilliseconds = 1000
            };

            return new object[] { codeTimer, true };
        }


        static object[] ActualGreaterThanExpectedCase() 
        {
            var timer = new TestPerformanceTimer(1200);
            var codeTimer = new CodeTimer("Case2", null, timer)
            {
                ExpectedMilliseconds = 1000
            };

            return new object[] { codeTimer, false };
        }

        static object[] ExpectedNotSetCase() 
        {
            var timer = new TestPerformanceTimer(1200);
            var codeTimer = new CodeTimer("Case2", null, timer);

            return new object[] { codeTimer, true };
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


    public class CodeTimerTestDataGenerator2 : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            NoMarkersAdded(),
            OneMarkerAdded()
        };

        static object[] NoMarkersAdded()
        {
            var timer = new TestPerformanceTimer(900);
            var codeTimer = new CodeTimer("NoMarkersAdded", null, timer);

            return new object[] { codeTimer, 0 };
        }

        static object[] OneMarkerAdded()
        {
            var timer = new TestPerformanceTimer(1000);
            var codeTimer = new CodeTimer("OneMarkerAdded", null, timer);

            codeTimer.Mark("Marker 1");

            return new object[] { codeTimer, 1 };
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
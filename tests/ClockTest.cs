using System;
using Xunit;

namespace Chronology
{
    public class ClockTest: TestFixture
    {
        readonly IClock sut = new Clock();

        public class Time: ClockTest
        {
            [Fact]
            public void ReturnsCurrentUtcDateTime() {
                DateTime expected = DateTime.UtcNow;
                UtcDateTime actual = sut.Time();
                Assert.Equal(expected, actual, TimeSpan.FromMilliseconds(1));
            }
        }
    }
}

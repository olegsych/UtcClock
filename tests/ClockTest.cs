using System;
using Xunit;

namespace Chronology
{
    public class ClockTest: TestFixture
    {
        readonly IClock sut = new Clock();

        static readonly TimeSpan clockPrecision = TimeSpan.FromMilliseconds(16);

        public class Time: ClockTest
        {
            [Fact]
            public void ReturnsCurrentUtcDateTime() {
                DateTime expected = DateTime.UtcNow;
                UtcDateTime actual = sut.Time;
                Assert.Equal(expected, actual, clockPrecision);
            }
        }
    }
}

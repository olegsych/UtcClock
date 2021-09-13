using System;
using Xunit;

namespace Chronology
{
    public class UniversalClockTest: TestFixture
    {
        readonly IClock<UtcDateTime> sut = new UniversalClock();

        static readonly TimeSpan clockPrecision = TimeSpan.FromMilliseconds(16);

        public class Time: UniversalClockTest
        {
            [Fact]
            public void ReturnsCurrentUtcDateTime() {
                DateTime expected = DateTime.UtcNow;
                UtcDateTime actual = sut.Now;
                Assert.Equal(expected, actual, clockPrecision);
            }
        }
    }
}

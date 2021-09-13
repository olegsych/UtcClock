using System;
using Xunit;

namespace Chronology
{
    public abstract class LocalClockTest: TestFixture
    {
        readonly IClock<DateTimeOffset> sut = new LocalClock();

        static readonly TimeSpan clockPrecision = TimeSpan.FromMilliseconds(16);

        public class Time: LocalClockTest
        {
            [Fact]
            public void ReturnsCurrentUtcDateTime() {
                DateTimeOffset expected = DateTimeOffset.Now;
                DateTimeOffset actual = sut.Now;
                Assert.Equal(expected.Offset, actual.Offset);
                Assert.Equal(expected.LocalDateTime, actual.LocalDateTime, clockPrecision);
            }
        }
    }
}

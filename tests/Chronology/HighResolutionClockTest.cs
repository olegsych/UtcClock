using System;
using System.Diagnostics;
using Inspector;
using Xunit;

namespace Chronology
{
    public abstract class HighResolutionClockTest
    {
        readonly IClock<TimeSpan> sut = new HighResolutionClock();

        static readonly TimeSpan tolerance = TimeSpan.FromMilliseconds(0.5);

        public class Now: HighResolutionClockTest
        {
            [Fact]
            public void ReturnsElapsedTimeOfRunningStopwatch() {
                Stopwatch stopwatch = typeof(HighResolutionClock).Field<Stopwatch>()!;

                TimeSpan expected = stopwatch.Elapsed;
                TimeSpan actual = sut.Now;

                Assert.True(stopwatch.IsRunning);
                Assert.True(actual - expected < tolerance);
            }
        }
    }
}

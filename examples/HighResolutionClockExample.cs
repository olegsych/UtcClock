using System;
using System.Threading;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Chronology
{
    public class HighResolutionClockExample
    {
        public class YourClass
        {
            readonly IClock<TimeSpan> clock;

            public YourClass(IClock<TimeSpan> clock) => this.clock = clock;

            public override string ToString() {
                TimeSpan start = clock.Now;
                Thread.Sleep(42);
                TimeSpan stop = clock.Now;
                TimeSpan elapsed = stop - start;
                return $"Elapsed time is {elapsed}";
            }
        }

        public static class YourApplication
        {
            public static void main() {
                var clock = new HighResolutionClock();
                var work = new YourClass(clock);
                Console.WriteLine(work);
            }
        }

        [Fact]
        public void YourTest() {
            IClock<TimeSpan> clock = Substitute.For<IClock<TimeSpan>>();
            var start = new TimeSpan(1);
            var stop = new TimeSpan(5);
            ConfiguredCall? arrange = clock.Now.Returns(start, stop);

            var sut = new YourClass(clock);
            string actual = sut.ToString();

            string expected = $"Elapsed time is {stop - start}";
            Assert.Equal(expected, actual);
        }
    }
}

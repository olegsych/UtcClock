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
            readonly IClock<HighResolutionTimestamp> clock;

            public YourClass(IClock<HighResolutionTimestamp> clock) =>
                this.clock = clock;

            public override string ToString() {
                HighResolutionTimestamp start = clock.Now;
                Thread.Sleep(42);
                HighResolutionTimestamp stop = clock.Now;
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
            IClock<HighResolutionTimestamp> clock = Substitute.For<IClock<HighResolutionTimestamp>>();
            var start = new HighResolutionTimestamp(1);
            var stop = new HighResolutionTimestamp(5);
            ConfiguredCall? arrange = clock.Now.Returns(start, stop);

            var sut = new YourClass(clock);
            string actual = sut.ToString();

            string expected = $"Elapsed time is {stop - start}";
            Assert.Equal(expected, actual);
        }
    }
}

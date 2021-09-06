using System;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Chronology
{
    public class ClockExample
    {
        public class YourClass
        {
            readonly IClock<UtcDateTime> clock;

            public YourClass(IClock<UtcDateTime> clock) => this.clock = clock;

            public override string ToString() {
                UtcDateTime now = clock.Now;
                return $"Current time is {now}";
            }
        }

        public static class YourApplication
        {
            public static void main() {
                var clock = new Clock();
                var work = new YourClass(clock);
                Console.WriteLine(work);
            }
        }

        [Fact]
        public void YourTest() {
            IClock<UtcDateTime> clock = Substitute.For<IClock<UtcDateTime>>();
            var time = new DateTime(2021, 9, 4, 12, 00, 00, DateTimeKind.Utc);
            ConfiguredCall? arrange = clock.Now.Returns(new UtcDateTime(time));

            var sut = new YourClass(clock);
            string actual = sut.ToString();

            string expected = $"Current time is {time:o}";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitConversions() {
            IClock<UtcDateTime> clock = new Clock();
            UtcDateTime utcDateTime = clock.Now;
            DateTime dateTime = utcDateTime;
            DateTimeOffset dateTimeOffset = utcDateTime;
        }
    }
}

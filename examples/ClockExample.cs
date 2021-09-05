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
            readonly IClock clock;

            public YourClass(IClock clock) => this.clock = clock;

            public override string ToString() {
                UtcDateTime now = clock.Time;
                return $"Current time is {now}";
            }
        }

        public class YourApplication
        {
            public static void Main() {
                var clock = new Clock();
                var work = new YourClass(clock);
                Console.WriteLine(work);
            }
        }

        [Fact]
        public void YourTest() {
            IClock clock = Substitute.For<IClock>();
            var time = new DateTime(2021, 9, 4, 12, 00, 00, DateTimeKind.Utc);
            ConfiguredCall? arrange = clock.Time.Returns(new UtcDateTime(time));

            var sut = new YourClass(clock);
            string actual = sut.ToString();

            string expected = $"Current time is {time:o}";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ImplicitConversions() {
            IClock clock = new Clock();
            UtcDateTime utcDateTime = clock.Time;
            DateTime dateTime = utcDateTime;
            DateTimeOffset dateTimeOffset = utcDateTime;
        }
    }
}

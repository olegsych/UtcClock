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
            public override string ToString() => $"Current time is {clock.Time}";
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
    }
}

using System;
using Fuzzy;
using Xunit;

namespace Chronology
{
    public class TimeSpanExtensionsTest: TestFixture
    {
        public class Days: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromDouble() {
                double value = fuzzy.TimeSpan().TotalDays;
                var expected = TimeSpan.FromDays(value);
                TimeSpan actual = value.Days();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromLong() {
                var value = (long)fuzzy.TimeSpan().TotalDays;
                var expected = TimeSpan.FromDays(value);
                TimeSpan actual = value.Days();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                var value = (int)fuzzy.TimeSpan().TotalDays;
                var expected = TimeSpan.FromDays(value);
                TimeSpan actual = value.Days();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                var value = (short)fuzzy.TimeSpan().TotalDays;
                var expected = TimeSpan.FromDays(value);
                TimeSpan actual = value.Days();
                Assert.Equal(expected, actual);
            }
        }

        public class Hours: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromDouble() {
                double value = fuzzy.TimeSpan().TotalHours;
                var expected = TimeSpan.FromHours(value);
                TimeSpan actual = value.Hours();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromLong() {
                var value = (long)fuzzy.TimeSpan().TotalHours;
                var expected = TimeSpan.FromHours(value);
                TimeSpan actual = value.Hours();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                var value = (int)fuzzy.TimeSpan().TotalHours;
                var expected = TimeSpan.FromHours(value);
                TimeSpan actual = value.Hours();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                var value = (short)fuzzy.TimeSpan().TotalHours;
                var expected = TimeSpan.FromHours(value);
                TimeSpan actual = value.Hours();
                Assert.Equal(expected, actual);
            }
        }

        public class Milliseconds: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromDouble() {
                double value = fuzzy.TimeSpan().TotalMilliseconds;
                var expected = TimeSpan.FromMilliseconds(value);
                TimeSpan actual = value.Milliseconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromLong() {
                var value = (long)fuzzy.TimeSpan().TotalMilliseconds;
                var expected = TimeSpan.FromMilliseconds(value);
                TimeSpan actual = value.Milliseconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                var value = (int)fuzzy.TimeSpan().TotalMilliseconds;
                var expected = TimeSpan.FromMilliseconds(value);
                TimeSpan actual = value.Milliseconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                var value = (short)fuzzy.TimeSpan().TotalMilliseconds;
                var expected = TimeSpan.FromMilliseconds(value);
                TimeSpan actual = value.Milliseconds();
                Assert.Equal(expected, actual);
            }
        }

        public class Minutes: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromDouble() {
                double value = fuzzy.TimeSpan().TotalMinutes;
                var expected = TimeSpan.FromMinutes(value);
                TimeSpan actual = value.Minutes();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromLong() {
                var value = (long)fuzzy.TimeSpan().TotalMinutes;
                var expected = TimeSpan.FromMinutes(value);
                TimeSpan actual = value.Minutes();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                var value = (int)fuzzy.TimeSpan().TotalMinutes;
                var expected = TimeSpan.FromMinutes(value);
                TimeSpan actual = value.Minutes();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                var value = (short)fuzzy.TimeSpan().TotalMinutes;
                var expected = TimeSpan.FromMinutes(value);
                TimeSpan actual = value.Minutes();
                Assert.Equal(expected, actual);
            }
        }


        public class Seconds: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromDouble() {
                double value = fuzzy.TimeSpan().TotalSeconds;
                var expected = TimeSpan.FromSeconds(value);
                TimeSpan actual = value.Seconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromLong() {
                var value = (long)fuzzy.TimeSpan().TotalSeconds;
                var expected = TimeSpan.FromSeconds(value);
                TimeSpan actual = value.Seconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                var value = (int)fuzzy.TimeSpan().TotalSeconds;
                var expected = TimeSpan.FromSeconds(value);
                TimeSpan actual = value.Seconds();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                var value = (short)fuzzy.TimeSpan().TotalSeconds;
                var expected = TimeSpan.FromSeconds(value);
                TimeSpan actual = value.Seconds();
                Assert.Equal(expected, actual);
            }
        }

        public class Ticks: TimeSpanExtensionsTest
        {
            [Fact]
            public void ReturnsTimeSpanFromLong() {
                long value = fuzzy.Int64();
                var expected = TimeSpan.FromTicks(value);
                TimeSpan actual = value.Ticks();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromInt() {
                int value = fuzzy.Int32();
                var expected = TimeSpan.FromTicks(value);
                TimeSpan actual = value.Ticks();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ReturnsTimeSpanFromShort() {
                short value = fuzzy.Int16();
                var expected = TimeSpan.FromTicks(value);
                TimeSpan actual = value.Ticks();
                Assert.Equal(expected, actual);
            }
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using Fuzzy;
using Xunit;

namespace Chronology
{
    public abstract class UtcDateTimeTest: TestFixture
    {
        readonly UtcDateTime sut;

        readonly DateTime inputDateTime = fuzzy.DateTime(DateTimeKind.Utc);

        protected UtcDateTimeTest() => sut = new UtcDateTime(inputDateTime);

        public sealed class TicksConstructor: UtcDateTimeTest
        {
            [Fact]
            public void CreatesValueFromLong() {
                DateTime input = fuzzy.DateTime(DateTimeKind.Utc);
                var sut = new UtcDateTime(input.Ticks);
                Assert.Equal(input.Ticks, sut.Ticks);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenInputIsLessThanZero() =>
                Assert.Throws<ArgumentOutOfRangeException>(() => new UtcDateTime(-1));

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenInputIsGreaterThanMax() =>
                Assert.Throws<ArgumentOutOfRangeException>(() => new UtcDateTime(DateTime.MaxValue.Ticks + 1));
        }

        public sealed class DateTimeOffsetConstructor: UtcDateTimeTest
        {
            [Fact]
            public void CreatesValueFromDateTimeOffset() {
                DateTimeOffset input = fuzzy.DateTimeOffset();
                var sut = new UtcDateTime(input);
                Assert.Equal(input.UtcDateTime.Ticks, sut.Ticks);
            }
        }

        public sealed class DateTimeConstructor: UtcDateTimeTest
        {
            [Fact]
            public void CreatesValueFromDateTimeWithUtcKind() {
                DateTime input = fuzzy.DateTime(DateTimeKind.Utc);
                var sut = new UtcDateTime(input);
                Assert.Equal(input.Ticks, sut.Ticks);
            }

            [Theory, InlineData(DateTimeKind.Local), InlineData(DateTimeKind.Unspecified)]
            public void ThrowsExceptionWhenDateTimeKindIsNotUtc(DateTimeKind kind) {
                DateTime input = fuzzy.DateTime(kind);
                var thrown = Assert.Throws<ArgumentException>(() => new UtcDateTime(input));
                Assert.Contains(nameof(DateTimeKind), thrown.Message, StringComparison.Ordinal);
                Assert.Contains(kind.ToString(), thrown.Message, StringComparison.Ordinal);
            }
        }

        public class EqualityOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreEqual() {
                UtcDateTime other = sut;
                Assert.True(sut == other);
                Assert.True(other == sut);
            }

            [Fact]
            public void ReturnsTrueWhenIninitializedIsEqualToDefault() {
                var initialized = new UtcDateTime(0);
                var uninitialized = default(UtcDateTime);
                Assert.True(initialized == uninitialized);
                Assert.True(uninitialized == initialized);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreDifferent() {
                UtcDateTime other = new UtcDateTime(fuzzy.DateTime(DateTimeKind.Utc));
                Assert.False(sut == other);
                Assert.False(other == sut);
            }

            [Fact]
            public void SupportsDateTimeThroughImplicitConversion() {
                DateTime other = sut;
                Assert.True(sut == other);
                Assert.True(other == sut);
            }

            [Fact]
            public void SupportsDateTimeOffsetThroughImplicitConversion() {
                DateTimeOffset other = sut;
                Assert.True(sut == other);
                Assert.True(other == sut);
            }
        }

        public class InequalityOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreDifferent() {
                UtcDateTime other = new UtcDateTime(fuzzy.DateTimeOffset());
                Assert.True(sut != other);
                Assert.True(other != sut);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreEqual() {
                UtcDateTime other = sut;
                Assert.False(sut != other);
                Assert.False(other != sut);
            }

            [Fact]
            public void ReturnsFalseWhenOneInitializedValueIsEqualToDefault() {
                var initialized = new UtcDateTime(0);
                var uninitialized = default(UtcDateTime);
                Assert.False(initialized != uninitialized);
                Assert.False(uninitialized != initialized);
            }

            [Fact]
            public void SupportsDateTimeThroughImplicitConversion() {
                DateTime other = new UtcDateTime(fuzzy.DateTimeOffset());
                Assert.True(sut != other);
                Assert.True(other != sut);
            }

            [Fact]
            public void SupportsDateTimeOffsetThroughImplicitConversion() {
                DateTimeOffset other = new UtcDateTime(fuzzy.DateTimeOffset());
                Assert.True(sut != other);
                Assert.True(other != sut);
            }
        }

        public class Add: UtcDateTimeTest
        {
            [Fact]
            public void AddsTimeSpanToUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = inputDateTime + timeSpan;
                UtcDateTime actual = sut.Add(timeSpan);
                Assert.Equal(expected.Ticks, actual.Ticks);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeLessThanMinimum() {
                var timeSpan = -new TimeSpan(inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Add(timeSpan));
                Assert.Equal("timeSpan", thrown.ParamName);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeGreaterThanMaximum() {
                var timeSpan = new TimeSpan(DateTime.MaxValue.Ticks - inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Add(timeSpan));
                Assert.Equal("timeSpan", thrown.ParamName);
            }
        }

        public class AdditionOperator: UtcDateTimeTest
        {
            [Fact]
            public void AddsTimeSpanToUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = inputDateTime + timeSpan;
                UtcDateTime actual = sut + timeSpan;
                Assert.Equal(expected.Ticks, actual.Ticks);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeLessThanMinimum() {
                var timeSpan = -new TimeSpan(inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut + timeSpan);
                Assert.Equal("timeSpan", thrown.ParamName);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeGreaterThanMaximum() {
                var timeSpan = new TimeSpan(DateTime.MaxValue.Ticks - inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut + timeSpan);
                Assert.Equal("timeSpan", thrown.ParamName);
            }
        }

        public class CompareTo: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsMinusOneWhenValueIsLessThanOther() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.Equal(-1, new UtcDateTime(left).CompareTo(new UtcDateTime(right)));
            }

            [Fact]
            public void ReturnsOneWhenValueIsGreaterThanOther() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.Equal(1, new UtcDateTime(left).CompareTo(new UtcDateTime(right)));
            }

            [Fact]
            public void ReturnsZeroWhenValueIsEqualToOther() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left;
                Assert.Equal(0, new UtcDateTime(left).CompareTo(new UtcDateTime(right)));
            }
        }

        public new class Equals: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueWhenOtherObjectIsEqual() {
                object other = sut;
                Assert.True(sut.Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherObjectIsDifferent() {
                object other = new UtcDateTime(fuzzy.DateTimeOffset());
                Assert.False(sut.Equals(other));
            }

            [Fact]
            [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "Equals is under test")]
            public void ReturnsFalseWhenOtherObjectIsNull() {
                object? other = null;
                Assert.False(sut.Equals(other!));
            }

            [Fact]
            public void ReturnsTrueWhenOtherUtcDateTimeIsEqual() {
                UtcDateTime other = sut;
                Assert.True(((IEquatable<UtcDateTime>)sut).Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherUtcDateTimeIsDifferent() {
                UtcDateTime other = new UtcDateTime(fuzzy.DateTimeOffset());
                Assert.False(((IEquatable<UtcDateTime>)sut).Equals(other));
            }
        }

        public new class GetHashCode: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsHashCodeOfDateTimeValue() =>
                Assert.Equal(inputDateTime.GetHashCode(), sut.GetHashCode());
        }

        public class GreaterThanOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new UtcDateTime(left) > new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new UtcDateTime(left) > new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left;
                Assert.False(new UtcDateTime(left) > new UtcDateTime(right));
            }
        }

        public class GreaterThanOrEqualOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new UtcDateTime(left) >= new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new UtcDateTime(left) >= new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left;
                Assert.True(new UtcDateTime(left) >= new UtcDateTime(right));
            }
        }

        public class LessThanOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new UtcDateTime(left) < new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new UtcDateTime(left) < new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left;
                Assert.False(new UtcDateTime(left) < new UtcDateTime(right));
            }
        }

        public class LessThanOrEqualOperator: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new UtcDateTime(left) <= new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new UtcDateTime(left) <= new UtcDateTime(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                DateTime left = fuzzy.DateTime(DateTimeKind.Utc);
                DateTime right = left;
                Assert.True(new UtcDateTime(left) <= new UtcDateTime(right));
            }
        }

        public class SubtractTimeSpan: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsTimeSpanFromUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = inputDateTime - timeSpan;
                UtcDateTime actual = sut.Subtract(timeSpan);
                Assert.Equal(expected.Ticks, actual.Ticks);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeLessThanMinimum() {
                var timeSpan = new TimeSpan(inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Subtract(timeSpan));
                Assert.Equal("timeSpan", thrown.ParamName);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeGreaterThanMaximum() {
                var timeSpan = -new TimeSpan(DateTime.MaxValue.Ticks - inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut.Subtract(timeSpan));
                Assert.Equal("timeSpan", thrown.ParamName);
            }
        }

        public class SubtractUtcDateTime: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                DateTime other = fuzzy.DateTime(DateTimeKind.Utc);
                TimeSpan expected = inputDateTime - other;
                TimeSpan actual = sut.Subtract(new UtcDateTime(other));
                Assert.Equal(expected, actual);
            }
        }

        public class SubtractTimeSpanOperator: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsTimeSpanFromUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = inputDateTime - timeSpan;
                UtcDateTime actual = sut - timeSpan;
                Assert.Equal(expected.Ticks, actual.Ticks);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeLessThanMinimum() {
                var timeSpan = new TimeSpan(inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut - timeSpan);
                Assert.Equal("timeSpan", thrown.ParamName);
            }

            [Fact]
            public void ThrowsArgumentOutOfRangeExceptionWhenResultWouldBeGreaterThanMaximum() {
                var timeSpan = -new TimeSpan(DateTime.MaxValue.Ticks - inputDateTime.Ticks + 1);
                var thrown = Assert.Throws<ArgumentOutOfRangeException>(() => sut - timeSpan);
                Assert.Equal("timeSpan", thrown.ParamName);
            }
        }

        public class SubtractUtcDateTimeOperator: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                DateTime other = fuzzy.DateTime(DateTimeKind.Utc);
                TimeSpan expected = inputDateTime - other;
                TimeSpan actual = sut - new UtcDateTime(other);
                Assert.Equal(expected, actual);
            }
        }

        public class ToDateTime: UtcDateTimeTest
        {
            [Fact]
            public void ConvertsInitializedValue() {
                DateTime actual = sut.ToDateTime();
                Assert.Equal(inputDateTime.Ticks, actual.Ticks);
                Assert.Equal(DateTimeKind.Utc, actual.Kind);
            }

            [Fact]
            public void ConvertsUninitializedValue() {
                DateTime actual = default(UtcDateTime).ToDateTime();
                Assert.Equal(0, actual.Ticks);
                Assert.Equal(DateTimeKind.Utc, actual.Kind);
            }
        }

        public class ToDateTimeOffset: UtcDateTimeTest
        {
            [Fact]
            public void ConvertsInitializedValue() {
                DateTimeOffset actual = sut.ToDateTimeOffset();
                Assert.Equal(inputDateTime.Ticks, actual.Ticks);
                Assert.Equal(TimeSpan.Zero, actual.Offset);
            }

            [Fact]
            public void ConvertsUninitializedValue() {
                DateTimeOffset actual = default(UtcDateTime).ToDateTimeOffset();
                Assert.Equal(0, actual.Ticks);
                Assert.Equal(TimeSpan.Zero, actual.Offset);
            }
        }

        public sealed class ToDateTimeConversionOperator: UtcDateTimeTest
        {
            [Fact]
            public void ConvertsInitializedValue() {
                DateTime actual = sut;
                Assert.Equal(sut.Ticks, actual.Ticks);
                Assert.Equal(DateTimeKind.Utc, actual.Kind);
            }

            [Fact]
            public void ConvertsUninitializedValue() {
                DateTime actual = default(UtcDateTime);
                Assert.Equal(0, actual.Ticks);
                Assert.Equal(DateTimeKind.Utc, actual.Kind);
            }
        }

        public sealed class ToDateTimeOffsetComversionOperator: UtcDateTimeTest
        {
            [Fact]
            public void ConvertsInitializedValue() {
                DateTimeOffset actual = sut;
                Assert.Equal(sut.Ticks, actual.Ticks);
                Assert.Equal(TimeSpan.Zero, actual.Offset);
            }

            [Fact]
            public void ConvertsUninitializedValue() {
                DateTimeOffset actual = default(UtcDateTime);
                Assert.Equal(0, actual.Ticks);
                Assert.Equal(TimeSpan.Zero, actual.Offset);
            }
        }

        public new class ToString: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsValueInRoundTripFormat() {
                string expected = inputDateTime.ToString("o");
                string? actual = sut.ToString();
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ConvertsUninitializedValueToUtc() {
                string expected = new DateTime(0, DateTimeKind.Utc).ToString("o");
                string? actual = default(UtcDateTime).ToString();
                Assert.Equal(expected, actual);
            }
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using Fuzzy;
using Inspector;
using Xunit;

namespace Chronology
{
    public abstract class UtcDateTimeTest: TestFixture
    {
        readonly UtcDateTime sut = new UtcDateTime(fuzzy.DateTimeOffset());

        public sealed class Constructor: UtcDateTimeTest
        {
            [Fact]
            public void CreatesValueFromDateTimeOffset() {
                DateTimeOffset input = fuzzy.DateTimeOffset();
                var sut = new UtcDateTime(input);
                Assert.Equal(input.UtcDateTime, sut.Field<DateTime>());
            }

            [Fact]
            public void CreatesValueFromDateTimeWithUtcKind() {
                DateTime input = fuzzy.DateTime(DateTimeKind.Utc);
                var sut = new UtcDateTime(input);
                Assert.Equal(input, sut.Field<DateTime>());
            }

            [Theory, InlineData(DateTimeKind.Local), InlineData(DateTimeKind.Unspecified)]
            public void ThrowsExceptionWhenDateTimeKindIsNotUtc(DateTimeKind kind) {
                DateTime input = fuzzy.DateTime(kind);
                var thrown = Assert.Throws<ArgumentException>(() => new UtcDateTime(input));
                Assert.Contains(nameof(DateTimeKind), thrown.Message, StringComparison.Ordinal);
                Assert.Contains(kind.ToString(), thrown.Message, StringComparison.Ordinal);
            }
        }

        public sealed class ImplicitConversionOperator: UtcDateTimeTest
        {
            [Fact]
            public void ConvertsToDateTime() {
                DateTime actual = sut;
                Assert.Equal(sut.Field<DateTime>(), actual);
            }

            [Fact]
            public void ConvertsToDateTimeOffset() {
                DateTimeOffset actual = sut;
                Assert.Equal(TimeSpan.Zero, actual.Offset);
                Assert.Equal(sut.Field<DateTime>(), actual.DateTime);
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
            public void ReturnsFalseWhenValuesAreDifferent() {
                UtcDateTime other = new UtcDateTime(fuzzy.DateTimeOffset());
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
                DateTime expected = sut.Field<DateTime>().Value + timeSpan;
                UtcDateTime actual = sut.Add(timeSpan);
                Assert.Equal(expected, actual.Field<DateTime>());
            }
        }

        public class AdditionOperator: UtcDateTimeTest
        {
            [Fact]
            public void AddsTimeSpanToUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = sut.Field<DateTime>().Value + timeSpan;
                UtcDateTime actual = sut + timeSpan;
                Assert.Equal(expected, actual.Field<DateTime>());
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
            public void ReturnsHashCodeOfDateTimeValue() {
                DateTime expected = sut.Field<DateTime>();
                Assert.Equal(expected.GetHashCode(), sut.GetHashCode());
            }
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

        public class Subtract: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsTimeSpanFromUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = sut.Field<DateTime>().Value - timeSpan;
                UtcDateTime actual = sut.Subtract(timeSpan);
                Assert.Equal(expected, actual.Field<DateTime>());
            }

            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                DateTime other = fuzzy.DateTime(DateTimeKind.Utc);
                TimeSpan expected = sut.Field<DateTime>().Value - other;
                TimeSpan actual = sut.Subtract(new UtcDateTime(other));
                Assert.Equal(expected, actual);
            }
        }

        public class SubtractionOperator: UtcDateTimeTest
        {
            [Fact]
            public void SubtractsTimeSpanFromUtcDateTime() {
                TimeSpan timeSpan = fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                DateTime expected = sut.Field<DateTime>().Value - timeSpan;
                UtcDateTime actual = sut - timeSpan;
                Assert.Equal(expected, actual.Field<DateTime>());
            }

            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                DateTime other = fuzzy.DateTime(DateTimeKind.Utc);
                TimeSpan expected = sut.Field<DateTime>().Value - other;
                TimeSpan actual = sut - new UtcDateTime(other);
                Assert.Equal(expected, actual);
            }
        }

        public class ToDateTime: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsDateTimeValue() {
                DateTime expected = sut.Field<DateTime>();
                Assert.Equal(expected, sut.ToDateTime());
            }
        }

        public class ToDateTimeOffset: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsDateTimeOffsetValue() {
                var expected = new DateTimeOffset(sut.Field<DateTime>());
                Assert.Equal(expected, sut.ToDateTimeOffset());
            }
        }

        public new class ToString: UtcDateTimeTest
        {
            [Fact]
            public void ReturnsValueInRoundTripFormat() {
                string expected = sut.Field<DateTime>().Value.ToString("o");
                string? actual = sut.ToString();
                Assert.Equal(expected, actual);
            }
        }
    }
}

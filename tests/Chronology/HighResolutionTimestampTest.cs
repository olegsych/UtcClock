using System;
using Fuzzy;
using Inspector;
using Xunit;

namespace Chronology
{
    public abstract class HighResolutionTimestampTest: TestFixture
    {
        readonly HighResolutionTimestamp sut;

        // Constructor parameters
        readonly TimeSpan timeSpan = fuzzy.TimeSpan();

        protected HighResolutionTimestampTest() => sut = new HighResolutionTimestamp(timeSpan);

        public class Constructor: HighResolutionTimestampTest
        {
            [Fact]
            public void StoresTimeSpanTicks() =>
                Assert.Equal(timeSpan.Ticks, sut.Field<long>().Value);
        }

        public class CompareTo: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsMinusOneWhenValueIsLessThanOther() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.Equal(-1, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }

            [Fact]
            public void ReturnsOneWhenValueIsGreaterThanOther() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.Equal(1, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }

            [Fact]
            public void ReturnsZeroWhenValueIsEqualToOther() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left;
                Assert.Equal(0, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }
        }

        public class EqualityOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreEqual() {
                var other = new HighResolutionTimestamp(timeSpan);
                Assert.True(sut == other);
                Assert.True(other == sut);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreDifferent() {
                var other = new HighResolutionTimestamp(fuzzy.TimeSpan());
                Assert.False(sut == other);
                Assert.False(other == sut);
            }
        }

        public new class Equals: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenOtherObjectIsEqual() {
                object other = sut;
                Assert.True(sut.Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherObjectIsDifferent() {
                object other = new HighResolutionTimestamp(fuzzy.TimeSpan());
                Assert.False(sut.Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherObjectIsNull() {
                object? other = null;
                Assert.False(sut.Equals(other!));
            }

            [Fact]
            public void ReturnsTrueWhenOtherHighResulutionTimestampIsEqual() {
                HighResolutionTimestamp other = sut;
                Assert.True(((IEquatable<HighResolutionTimestamp>)sut).Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherHighResolutionTimestampIsDifferent() {
                var other = new HighResolutionTimestamp(fuzzy.TimeSpan());
                Assert.False(((IEquatable<HighResolutionTimestamp>)sut).Equals(other));
            }
        }

        public new class GetHashCode: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsHashCodeOfTimeSpanValue() =>
                Assert.Equal(timeSpan.GetHashCode(), sut.GetHashCode());
        }

        public class GreaterThanOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left;
                Assert.False(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }
        }

        public class GreaterThanOrEqualOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left;
                Assert.True(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }
        }

        public class InequalityOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreDifferent() {
                var other = new HighResolutionTimestamp(fuzzy.TimeSpan());
                Assert.True(sut != other);
                Assert.True(other != sut);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreEqual() {
                var other = new HighResolutionTimestamp(timeSpan);
                Assert.False(sut != other);
                Assert.False(other != sut);
            }
        }

        public class LessThanOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left;
                Assert.False(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }
        }

        public class LessThanOrEqualOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left + fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.True(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left - fuzzy.TimeSpan().Between(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20));
                Assert.False(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                TimeSpan left = fuzzy.TimeSpan();
                TimeSpan right = left;
                Assert.True(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }
        }

        public class Subtract: HighResolutionTimestampTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                TimeSpan other = fuzzy.TimeSpan();
                TimeSpan expected = timeSpan - other;
                TimeSpan actual = sut.Subtract(new HighResolutionTimestamp(other));
                Assert.Equal(expected, actual);
            }
        }

        public class SubtractOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnsTimeSpan() {
                TimeSpan other = fuzzy.TimeSpan();
                TimeSpan expected = timeSpan - other;
                TimeSpan actual = sut - new HighResolutionTimestamp(other);
                Assert.Equal(expected, actual);
            }
        }

    }
}

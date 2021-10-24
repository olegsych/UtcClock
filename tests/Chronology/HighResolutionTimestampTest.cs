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
        readonly long ticks = fuzzy.Int64();

        protected HighResolutionTimestampTest() => sut = new HighResolutionTimestamp(ticks);

        public class Constructor: HighResolutionTimestampTest
        {
            [Fact]
            public void StoreslongTicks() =>
                Assert.Equal(ticks, sut.Field<long>().Value);
        }

        public class CompareTo: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsMinusOneWhenValueIsLessThanOther() {
                long left = fuzzy.Int64();
                long right = left + fuzzy.Int64().Between(10, 20);
                Assert.Equal(-1, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }

            [Fact]
            public void ReturnsOneWhenValueIsGreaterThanOther() {
                long left = fuzzy.Int64();
                long right = left - fuzzy.Int64().Between(10, 20);
                Assert.Equal(1, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }

            [Fact]
            public void ReturnsZeroWhenValueIsEqualToOther() {
                long left = fuzzy.Int64();
                long right = left;
                Assert.Equal(0, new HighResolutionTimestamp(left).CompareTo(new HighResolutionTimestamp(right)));
            }
        }

        public class EqualityOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreEqual() {
                var other = new HighResolutionTimestamp(ticks);
                Assert.True(sut == other);
                Assert.True(other == sut);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreDifferent() {
                var other = new HighResolutionTimestamp(fuzzy.Int64());
                Assert.False(sut == other);
                Assert.False(other == sut);
            }
        }

        public new class Equals: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenOtherObjectIsEqual() {
                object other = new HighResolutionTimestamp(ticks);
                Assert.True(sut.Equals(other));
            }

            [Fact]
            public void ReturnsFalseWhenOtherObjectIsDifferent() {
                object other = new HighResolutionTimestamp(fuzzy.Int64());
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
                var other = new HighResolutionTimestamp(fuzzy.Int64());
                Assert.False(((IEquatable<HighResolutionTimestamp>)sut).Equals(other));
            }
        }

        public new class GetHashCode: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsHashCodeOflongValue() =>
                Assert.Equal(ticks.GetHashCode(), sut.GetHashCode());
        }

        public class GreaterThanOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                long left = fuzzy.Int64();
                long right = left - fuzzy.Int64().Between(10, 20);
                Assert.True(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                long left = fuzzy.Int64();
                long right = left + fuzzy.Int64().Between(10, 20);
                Assert.False(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                long left = fuzzy.Int64();
                long right = left;
                Assert.False(new HighResolutionTimestamp(left) > new HighResolutionTimestamp(right));
            }
        }

        public class GreaterThanOrEqualOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsGreaterThanRightValue() {
                long left = fuzzy.Int64();
                long right = left - fuzzy.Int64().Between(10, 20);
                Assert.True(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsLessThanRightValue() {
                long left = fuzzy.Int64();
                long right = left + fuzzy.Int64().Between(10, 20);
                Assert.False(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                long left = fuzzy.Int64();
                long right = left;
                Assert.True(new HighResolutionTimestamp(left) >= new HighResolutionTimestamp(right));
            }
        }

        public class InequalityOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueWhenValuesAreDifferent() {
                var other = new HighResolutionTimestamp(fuzzy.Int64());
                Assert.True(sut != other);
                Assert.True(other != sut);
            }

            [Fact]
            public void ReturnsFalseWhenValuesAreEqual() {
                var other = new HighResolutionTimestamp(ticks);
                Assert.False(sut != other);
                Assert.False(other != sut);
            }
        }

        public class LessThanOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                long left = fuzzy.Int64();
                long right = left + fuzzy.Int64().Between(10, 20);
                Assert.True(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                long left = fuzzy.Int64();
                long right = left - fuzzy.Int64().Between(10, 20);
                Assert.False(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsEqualToRightValue() {
                long left = fuzzy.Int64();
                long right = left;
                Assert.False(new HighResolutionTimestamp(left) < new HighResolutionTimestamp(right));
            }
        }

        public class LessThanOrEqualOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void ReturnsTrueIfLeftValueIsLessThanRightValue() {
                long left = fuzzy.Int64();
                long right = left + fuzzy.Int64().Between(10, 20);
                Assert.True(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsFalseIfLeftValueIsGreaterThanRightValue() {
                long left = fuzzy.Int64();
                long right = left - fuzzy.Int64().Between(10, 20);
                Assert.False(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }

            [Fact]
            public void ReturnsTrueIfLeftValueIsEqualToRightValue() {
                long left = fuzzy.Int64();
                long right = left;
                Assert.True(new HighResolutionTimestamp(left) <= new HighResolutionTimestamp(right));
            }
        }

        public class Subtract: HighResolutionTimestampTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnslong() {
                long other = fuzzy.Int64();
                long expected = ticks - other;
                TimeSpan actual = sut.Subtract(new HighResolutionTimestamp(other));
                Assert.Equal(expected, actual.Ticks);
            }
        }

        public class SubtractOperator: HighResolutionTimestampTest
        {
            [Fact]
            public void SubtractsUtcDateTimeAndReturnslong() {
                long other = fuzzy.Int64();
                long expected = ticks - other;
                TimeSpan actual = sut - new HighResolutionTimestamp(other);
                Assert.Equal(expected, actual.Ticks);
            }
        }

    }
}

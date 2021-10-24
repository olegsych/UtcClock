using System;
using System.Linq.Expressions;
using Chronology;
using Inspector;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Fuzzy.Implementation
{
    public class FuzzyUtcDateTimeTest: TestFixture
    {
        readonly FuzzyRange<UtcDateTime> sut;

        public FuzzyUtcDateTimeTest() =>
            sut = new FuzzyUtcDateTime(fuzzy);

        public class Constructor: FuzzyUtcDateTimeTest
        {
            [Fact]
            public void InitializesBaseClass() {
                Assert.Same(fuzzy, sut.Field<IFuzz>().Value);
                Assert.Equal(DateTime.MinValue, sut.Minimum);
                Assert.Equal(DateTime.MaxValue, sut.Maximum);
            }
        }

        public class Build: FuzzyUtcDateTimeTest
        {
            public Build() => ArrangeBuildOfFuzzyInt64();

            [Fact]
            public void ReturnsUtcDateTimeCreatedFromFuzzyLongValue() {
                sut.Minimum = new UtcDateTime(random.Next());
                sut.Maximum = new UtcDateTime(sut.Minimum.Ticks + random.Next());
                long expectedTicks = random.Next();
                Expression<Predicate<FuzzyRange<long>>> fuzzyTicks = v => v.Minimum == sut.Minimum.Ticks && v.Maximum == sut.Maximum.Ticks;
                ConfiguredCall arrange = fuzzy.Build(Arg.Is(fuzzyTicks)).Returns(expectedTicks);

                UtcDateTime actual = InvokeBuild(sut);

                Assert.Equal(expectedTicks, actual.Ticks);
            }

            static UtcDateTime InvokeBuild(Fuzzy<UtcDateTime> sut) =>
                sut.Protected().Method<Func<UtcDateTime>>("Build").Invoke();
        }
    }
}

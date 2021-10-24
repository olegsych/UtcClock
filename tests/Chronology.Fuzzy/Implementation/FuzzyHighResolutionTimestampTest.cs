using System;
using System.Linq.Expressions;
using Chronology;
using Inspector;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Fuzzy.Implementation
{
    public class FuzzyHighResolutionTimestampTest: TestFixture
    {
        readonly FuzzyRange<HighResolutionTimestamp> sut;

        public FuzzyHighResolutionTimestampTest() =>
            sut = new FuzzyHighResolutionTimestamp(fuzzy);

        public class Constructor: FuzzyHighResolutionTimestampTest
        {
            [Fact]
            public void InitializesBaseClass() {
                Assert.Same(fuzzy, sut.Field<IFuzz>().Value);
                Assert.Equal(HighResolutionTimestamp.MinValue, sut.Minimum);
                Assert.Equal(HighResolutionTimestamp.MaxValue, sut.Maximum);
            }
        }

        public class Build: FuzzyHighResolutionTimestampTest
        {
            public Build() => ArrangeBuildOfFuzzyInt64();

            [Fact]
            public void ReturnsDateTimeCreatedFromFuzzyLongAndDateTimeKindValues() {
                sut.Minimum = new HighResolutionTimestamp(random.Next());
                sut.Maximum = new HighResolutionTimestamp(sut.Minimum.Ticks + random.Next());
                long expectedTicks = random.Next();
                Expression<Predicate<FuzzyRange<long>>> fuzzyTicks = v => v.Minimum == sut.Minimum.Ticks && v.Maximum == sut.Maximum.Ticks;
                ConfiguredCall arrange = fuzzy.Build(Arg.Is(fuzzyTicks)).Returns(expectedTicks);

                HighResolutionTimestamp actual = InvokeBuild(sut);

                Assert.Equal(expectedTicks, actual.Ticks);
            }

            static HighResolutionTimestamp InvokeBuild(Fuzzy<HighResolutionTimestamp> sut) =>
                sut.Protected().Method<Func<HighResolutionTimestamp>>("Build").Invoke();
        }
    }
}

using Fuzzy.Implementation;
using Inspector;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Fuzzy
{
    public abstract class IFuzzExtensionsTest: TestFixture
    {
        public class UtcDateTime: IFuzzExtensionsTest
        {
            [Fact]
            public void ReturnsValueBuiltByFuzzyUtcDateTime() {
                FuzzyUtcDateTime? actualSpec = null;
                var expectedValue = new Chronology.UtcDateTime(random.Next());
                ConfiguredCall arrange = fuzzy.Build(Arg.Do<FuzzyUtcDateTime>(spec => actualSpec = spec)).Returns(expectedValue);

                Chronology.UtcDateTime actualValue = fuzzy.UtcDateTime();

                Assert.Equal(expectedValue, actualValue);
                Assert.Same(fuzzy, actualSpec!.Field<IFuzz>().Value);
            }
        }
    }
}

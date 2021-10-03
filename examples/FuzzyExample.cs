using Fuzzy;
using Xunit;

namespace Chronology
{
    public class FuzzyExample
    {
        readonly IFuzz fuzzy = new RandomFuzz();

        [Fact]
        public void FuzzyUtcDateTime() {
            UtcDateTime value;
            value = fuzzy.UtcDateTime();
            value = fuzzy.UtcDateTime().Minimum(new UtcDateTime(2021, 10, 3));
            value = fuzzy.UtcDateTime().Maximum(new UtcDateTime(2021, 10, 4));
            value = fuzzy.UtcDateTime().Between(new UtcDateTime(2021, 10, 3), new UtcDateTime(2021, 10, 4));
            value = fuzzy.UtcDateTime().Between(new UtcDateTime(2021, 10, 3), 5.Minutes());
        }
    }
}

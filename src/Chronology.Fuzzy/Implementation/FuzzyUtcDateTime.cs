using Chronology;

namespace Fuzzy.Implementation
{
    sealed class FuzzyUtcDateTime: FuzzyRange<UtcDateTime>
    {
        public FuzzyUtcDateTime(IFuzz fuzzy) :
            base(fuzzy, UtcDateTime.MinValue, UtcDateTime.MaxValue) { }

        protected override UtcDateTime Build() {
            long ticks = fuzzy.Int64().Between(Minimum.Ticks, Maximum.Ticks);
            return new UtcDateTime(ticks);
        }
    }
}

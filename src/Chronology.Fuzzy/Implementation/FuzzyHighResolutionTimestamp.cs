using Chronology;

namespace Fuzzy.Implementation
{
    sealed class FuzzyHighResolutionTimestamp: FuzzyRange<HighResolutionTimestamp>
    {
        public FuzzyHighResolutionTimestamp(IFuzz fuzzy) :
            base(fuzzy, HighResolutionTimestamp.MinValue, HighResolutionTimestamp.MaxValue) { }

        protected override HighResolutionTimestamp Build() {
            long ticks = fuzzy.Int64().Between(Minimum.Ticks, Maximum.Ticks);
            return new HighResolutionTimestamp(ticks);
        }
    }
}

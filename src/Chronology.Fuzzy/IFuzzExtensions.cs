using Chronology;
using Fuzzy.Implementation;

namespace Fuzzy
{
    public static class IFuzzExtensions
    {
        public static HighResolutionTimestamp HighResolutionTimestamp(this IFuzz fuzzy) =>
            new FuzzyHighResolutionTimestamp(fuzzy);

        public static UtcDateTime UtcDateTime(this IFuzz fuzzy) =>
            new FuzzyUtcDateTime(fuzzy);
    }
}

using Chronology;
using Fuzzy.Implementation;

namespace Fuzzy
{
    public static class IFuzzExtensions
    {
        public static UtcDateTime UtcDateTime(this IFuzz fuzzy) => new FuzzyUtcDateTime(fuzzy);
    }
}

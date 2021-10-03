using System;
using Chronology;

namespace Fuzzy
{
    public static class UtcDateTimeExtensions
    {
        public static UtcDateTime Between(this UtcDateTime value, UtcDateTime minimum, TimeSpan timeSpan) =>
            IComparableExtensions.Between(value, minimum, minimum + timeSpan);
    }
}

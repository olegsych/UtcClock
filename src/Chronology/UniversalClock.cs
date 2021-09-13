using System;

namespace Chronology
{
    public sealed class UniversalClock: IClock<UtcDateTime>
    {
        UtcDateTime IClock<UtcDateTime>.Now => new UtcDateTime(DateTime.UtcNow);
    }
}

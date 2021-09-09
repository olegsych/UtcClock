using System;

namespace Chronology
{
    public sealed class LocalClock: IClock<DateTimeOffset>
    {
        DateTimeOffset IClock<DateTimeOffset>.Now => DateTimeOffset.Now;
    }
}

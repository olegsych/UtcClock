using System;

namespace Chronology
{
    public sealed class Clock: IClock<UtcDateTime>
    {
        UtcDateTime IClock<UtcDateTime>.Now => new UtcDateTime(DateTime.UtcNow);
    }
}

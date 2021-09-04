using System;

namespace Chronology
{
    public sealed class Clock: IClock
    {
        UtcDateTime IClock.Time() => new UtcDateTime(DateTime.UtcNow);
    }
}

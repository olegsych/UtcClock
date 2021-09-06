using System;

namespace Chronology
{
    public sealed class Clock: IClock<UtcDateTime>
    {
        UtcDateTime IClock<UtcDateTime>.Time => new UtcDateTime(DateTime.UtcNow);
    }
}

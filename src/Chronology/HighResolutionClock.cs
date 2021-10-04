using System;
using System.Diagnostics;

namespace Chronology
{
    /// <summary>
    /// Returns high-resolution timestamps.
    /// </summary>
    /// <remarks>
    /// The <see cref="TimeSpan"/> values returned by the <see cref="HighResolutionClock"/> are more granular than
    /// values returned by the <see cref="UniversalClock"/> or the <see cref="LocalClock"/>. They allow measuring
    /// short durations and ordering telemetry items collected on a single multi-core machine with more precision.
    /// However, the <see cref="HighResolutionClock"/> is not synchronized with internet time and loses accuracy
    /// over longer periods of time.
    /// </remarks>
    public sealed class HighResolutionClock: IClock<TimeSpan>
    {
        static readonly Stopwatch stopwatch = Stopwatch.StartNew();

        TimeSpan IClock<TimeSpan>.Now => stopwatch.Elapsed;
    }
}

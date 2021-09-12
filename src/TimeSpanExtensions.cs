using System;

namespace Chronology
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Days(this double value) => TimeSpan.FromDays(value);
        public static TimeSpan Days(this long value) => TimeSpan.FromDays(value);
        public static TimeSpan Days(this int value) => TimeSpan.FromDays(value);
        public static TimeSpan Days(this short value) => TimeSpan.FromDays(value);

        public static TimeSpan Hours(this double value) => TimeSpan.FromHours(value);
        public static TimeSpan Hours(this long value) => TimeSpan.FromHours(value);
        public static TimeSpan Hours(this int value) => TimeSpan.FromHours(value);
        public static TimeSpan Hours(this short value) => TimeSpan.FromHours(value);

        public static TimeSpan Milliseconds(this double value) => TimeSpan.FromMilliseconds(value);
        public static TimeSpan Milliseconds(this long value) => TimeSpan.FromMilliseconds(value);
        public static TimeSpan Milliseconds(this int value) => TimeSpan.FromMilliseconds(value);
        public static TimeSpan Milliseconds(this short value) => TimeSpan.FromMilliseconds(value);

        public static TimeSpan Minutes(this double value) => TimeSpan.FromMinutes(value);
        public static TimeSpan Minutes(this long value) => TimeSpan.FromMinutes(value);
        public static TimeSpan Minutes(this int value) => TimeSpan.FromMinutes(value);
        public static TimeSpan Minutes(this short value) => TimeSpan.FromMinutes(value);

        public static TimeSpan Seconds(this double value) => TimeSpan.FromSeconds(value);
        public static TimeSpan Seconds(this long value) => TimeSpan.FromSeconds(value);
        public static TimeSpan Seconds(this int value) => TimeSpan.FromSeconds(value);
        public static TimeSpan Seconds(this short value) => TimeSpan.FromSeconds(value);

        public static TimeSpan Ticks(this long value) => TimeSpan.FromTicks(value);
        public static TimeSpan Ticks(this int value) => TimeSpan.FromTicks(value);
        public static TimeSpan Ticks(this short value) => TimeSpan.FromTicks(value);
    }
}

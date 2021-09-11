using System;

namespace Chronology
{
    public struct UtcDateTime: IEquatable<UtcDateTime>, IComparable<UtcDateTime>
    {
        static readonly long minTicks = DateTime.MinValue.Ticks;
        static readonly long maxTicks = DateTime.MaxValue.Ticks;

        // Don't use auto-property; access field for maximum performance.
        #pragma warning disable IDE0032
        readonly long ticks;
        #pragma warning restore IDE0032

        public UtcDateTime(long ticks) {
            if(ticks < minTicks || ticks > maxTicks)
                throw new ArgumentOutOfRangeException(nameof(ticks));
            this.ticks = ticks;
        }

        public UtcDateTime(DateTimeOffset value) =>
            ticks = value.UtcDateTime.Ticks;

        public UtcDateTime(DateTime value) {
            if(value.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(DateTimeKind)}.{value.Kind} is not supported.", nameof(value));
            ticks = value.ToUniversalTime().Ticks;
        }

        public static readonly UtcDateTime MaxValue = new UtcDateTime(DateTimeOffset.MaxValue.UtcDateTime);

        public static readonly UtcDateTime MinValue = new UtcDateTime(DateTimeOffset.MinValue.UtcDateTime);

        public long Ticks => ticks;

        public UtcDateTime Add(TimeSpan timeSpan) =>
            this + timeSpan;

        public int CompareTo(UtcDateTime other) =>
            ticks < other.ticks ? -1 :
            ticks > other.ticks ? 1 :
            0;

        public override bool Equals(object obj) =>
            obj is UtcDateTime other
            ? Equals(other)
            : false;

        public bool Equals(UtcDateTime other) =>
            ticks.Equals(other.ticks);

        public override int GetHashCode() =>
            ticks.GetHashCode();

        public UtcDateTime Subtract(TimeSpan timeSpan) =>
            this - timeSpan;

        public TimeSpan Subtract(UtcDateTime value) =>
            this - value;

        public DateTime ToDateTime() =>
            this;

        public DateTimeOffset ToDateTimeOffset() =>
            this;

        public override string ToString() =>
            ToDateTime().ToString("o");

        public static implicit operator DateTime(UtcDateTime utc) =>
            new DateTime(utc.ticks, DateTimeKind.Utc);

        public static implicit operator DateTimeOffset(UtcDateTime utc) =>
            new DateTimeOffset(utc.ticks, TimeSpan.Zero);

        public static bool operator ==(UtcDateTime left, UtcDateTime right) =>
            left.ticks == right.ticks;

        public static bool operator !=(UtcDateTime left, UtcDateTime right) =>
            left.ticks != right.ticks;

        public static bool operator <(UtcDateTime left, UtcDateTime right) =>
            left.ticks < right.ticks;

        public static bool operator <=(UtcDateTime left, UtcDateTime right) =>
            left.ticks <= right.ticks;

        public static bool operator >(UtcDateTime left, UtcDateTime right) =>
            left.ticks > right.ticks;

        public static bool operator >=(UtcDateTime left, UtcDateTime right) =>
            left.ticks >= right.ticks;

        public static UtcDateTime operator +(UtcDateTime utc, TimeSpan timeSpan) =>
            timeSpan.Ticks < -utc.ticks || timeSpan.Ticks > maxTicks - utc.ticks
            ? throw new ArgumentOutOfRangeException(nameof(timeSpan))
            : new UtcDateTime(utc.ticks + timeSpan.Ticks);

        public static UtcDateTime operator -(UtcDateTime utc, TimeSpan timeSpan) =>
            timeSpan.Ticks > utc.ticks || timeSpan.Ticks < utc.ticks - maxTicks
            ? throw new ArgumentOutOfRangeException(nameof(timeSpan))
            : new UtcDateTime(utc.ticks - timeSpan.Ticks);

        public static TimeSpan operator -(UtcDateTime left, UtcDateTime right) =>
            new TimeSpan(left.ticks - right.ticks);
    }
}

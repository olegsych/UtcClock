using System;

namespace Chronology
{
    public readonly struct HighResolutionTimestamp: IEquatable<HighResolutionTimestamp>, IComparable<HighResolutionTimestamp>
    {
        // Don't use auto property; use field for maximum performance.
        #pragma warning disable IDE0032
        readonly long ticks;
        #pragma warning restore IDE0032

        public HighResolutionTimestamp(long ticks) =>
            this.ticks = ticks;

        public long Ticks => ticks;

        public int CompareTo(HighResolutionTimestamp other) =>
            ticks < other.ticks ? -1 :
            ticks > other.ticks ? 1 :
            0;

        public override bool Equals(object obj) =>
            obj is HighResolutionTimestamp other
            ? Equals(other)
            : false;

        public bool Equals(HighResolutionTimestamp other) =>
            ticks.Equals(other.ticks);

        public override int GetHashCode() =>
            ticks.GetHashCode();

        public TimeSpan Subtract(HighResolutionTimestamp value) =>
            new TimeSpan(ticks - value.ticks);

        public override string ToString() =>
            ticks.ToString();

        public static bool operator ==(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks == right.ticks;

        public static bool operator !=(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks != right.ticks;

        public static bool operator <(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks < right.ticks;

        public static bool operator <=(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks <= right.ticks;

        public static bool operator >(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks > right.ticks;

        public static bool operator >=(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            left.ticks >= right.ticks;

        public static TimeSpan operator -(HighResolutionTimestamp left, HighResolutionTimestamp right) =>
            new TimeSpan(left.ticks - right.ticks);
    }
}

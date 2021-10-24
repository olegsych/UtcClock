using System;

namespace Chronology
{
    public readonly struct HighResolutionTimestamp: IEquatable<HighResolutionTimestamp>, IComparable<HighResolutionTimestamp>
    {
        readonly long ticks;

        public HighResolutionTimestamp(TimeSpan value) =>
            ticks = value.Ticks;

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

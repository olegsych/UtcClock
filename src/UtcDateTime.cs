using System;

namespace Chronology
{
    public struct UtcDateTime: IEquatable<UtcDateTime>, IComparable<UtcDateTime>
    {
        readonly DateTime value;

        public UtcDateTime(long ticks) => value = new DateTime(ticks, DateTimeKind.Utc);

        public UtcDateTime(DateTimeOffset value) => this.value = value.UtcDateTime;

        public UtcDateTime(DateTime value) {
            if(value.Kind != DateTimeKind.Utc)
                throw new ArgumentException($"{nameof(DateTimeKind)}.{value.Kind} is not supported.", nameof(value));
            this.value = value.ToUniversalTime();
        }

        public UtcDateTime Add(TimeSpan value) => this + value;
        public int CompareTo(UtcDateTime other) => value < other.value ? -1 : value > other.value ? 1 : 0;
        public override bool Equals(object obj) => obj is UtcDateTime other ? Equals(other) : false;
        public bool Equals(UtcDateTime other) => value.Equals(other.value);
        public override int GetHashCode() => value.GetHashCode();
        public UtcDateTime Subtract(TimeSpan value) => this - value;
        public TimeSpan Subtract(UtcDateTime value) => this - value;
        public DateTime ToDateTime() => this;
        public DateTimeOffset ToDateTimeOffset() => this;
        public override string ToString() => ToDateTime().ToString("o");

        public static implicit operator DateTime(UtcDateTime utc) => utc.value.Kind == DateTimeKind.Utc ? utc.value : new DateTime(0, DateTimeKind.Utc);
        public static implicit operator DateTimeOffset(UtcDateTime utc) => utc.value.Kind == DateTimeKind.Utc ? utc.value : new DateTimeOffset(0, TimeSpan.Zero);
        public static bool operator ==(UtcDateTime left, UtcDateTime right) => left.value == right.value;
        public static bool operator !=(UtcDateTime left, UtcDateTime right) => left.value != right.value;
        public static UtcDateTime operator +(UtcDateTime left, TimeSpan right) => new UtcDateTime(left.value + right);
        public static UtcDateTime operator -(UtcDateTime left, TimeSpan right) => new UtcDateTime(left.value - right);
        public static TimeSpan operator -(UtcDateTime left, UtcDateTime right) => left.value - right.value;
        public static bool operator <(UtcDateTime left, UtcDateTime right) => left.value < right.value;
        public static bool operator <=(UtcDateTime left, UtcDateTime right) => left.value <= right.value;
        public static bool operator >(UtcDateTime left, UtcDateTime right) => left.value > right.value;
        public static bool operator >=(UtcDateTime left, UtcDateTime right) => left.value >= right.value;
    }
}

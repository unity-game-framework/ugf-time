using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Time.Runtime
{
    /// <summary>
    /// Represents serializable ticks information.
    /// </summary>
    [Serializable]
    public struct TimeTicks : IEquatable<TimeTicks>, IComparable<TimeTicks>
    {
        [SerializeField] private long m_value;

        /// <summary>
        /// Gets the total ticks.
        /// </summary>
        public long Value { get { return m_value; } }

        /// <summary>
        /// Gets the total seconds.
        /// </summary>
        public float TotalSeconds { get { return m_value * SecondsPerTick; } }

        /// <summary>
        /// Gets this instance as <see cref="TimeSpan"/>.
        /// </summary>
        public TimeSpan AsTimeSpan { get { return this; } }

        /// <summary>
        /// Represents the zero value of <see cref="TimeTicks"/>.
        /// </summary>
        public static readonly TimeTicks Zero = new TimeTicks(0L);

        /// <summary>
        /// Represents the smallest possible value of <see cref="TimeTicks"/>.
        /// </summary>
        public static readonly TimeTicks MinValue = new TimeTicks(long.MinValue);

        /// <summary>
        /// Represents the largest possible value of <see cref="TimeTicks"/>.
        /// </summary>
        public static readonly TimeTicks MaxValue = new TimeTicks(long.MaxValue);

        /// <summary>
        /// Represents the number of ticks per second.
        /// </summary>
        public const long TicksPerSecond = TimeSpan.TicksPerSecond;

        /// <summary>
        /// Represents the number of seconds per tick.
        /// </summary>
        public const float SecondsPerTick = 1.0F / TicksPerSecond;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public TimeSpan TimeSpan;
            [FieldOffset(0)] public TimeTicks TimeTicks;
        }

        /// <summary>
        /// Creates ticks from the specified ticks.
        /// </summary>
        /// <param name="value">The total ticks.</param>
        public TimeTicks(long value)
        {
            m_value = value;
        }

        public bool Equals(TimeTicks other)
        {
            return m_value == other.m_value;
        }

        public override bool Equals(object obj)
        {
            return obj is TimeTicks other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        public int CompareTo(TimeTicks other)
        {
            return m_value.CompareTo(other.m_value);
        }

        /// <summary>
        /// Creates ticks from the specified ticks.
        /// </summary>
        /// <param name="ticks">The total ticks.</param>
        public static TimeTicks FromTicks(long ticks)
        {
            return new TimeTicks(ticks);
        }

        /// <summary>
        /// Creates ticks from the specified seconds.
        /// </summary>
        /// <param name="seconds">The total seconds to convert to ticks.</param>
        public static TimeTicks FromSeconds(float seconds)
        {
            return new TimeTicks((long)(seconds * TicksPerSecond));
        }

        public static bool operator ==(TimeTicks left, TimeTicks right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeTicks left, TimeTicks right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(TimeTicks left, TimeTicks right)
        {
            return left.m_value < right.m_value;
        }

        public static bool operator >(TimeTicks left, TimeTicks right)
        {
            return left.m_value > right.m_value;
        }

        public static bool operator <=(TimeTicks left, TimeTicks right)
        {
            return left.m_value <= right.m_value;
        }

        public static bool operator >=(TimeTicks left, TimeTicks right)
        {
            return left.m_value >= right.m_value;
        }

        public static TimeTicks operator -(TimeTicks left)
        {
            return new TimeTicks(checked(-left.m_value));
        }

        public static TimeTicks operator -(TimeTicks left, TimeTicks right)
        {
            return new TimeTicks(checked(left.m_value - right.m_value));
        }

        public static TimeTicks operator -(TimeTicks left, long right)
        {
            return new TimeTicks(checked(left.m_value - right));
        }

        public static TimeTicks operator -(long left, TimeTicks right)
        {
            return new TimeTicks(checked(left - right.m_value));
        }

        public static TimeTicks operator +(TimeTicks left)
        {
            return left;
        }

        public static TimeTicks operator +(TimeTicks left, TimeTicks right)
        {
            return new TimeTicks(checked(left.m_value + right.m_value));
        }

        public static TimeTicks operator +(TimeTicks left, long right)
        {
            return new TimeTicks(checked(left.m_value + right));
        }

        public static TimeTicks operator +(long left, TimeTicks right)
        {
            return new TimeTicks(checked(left + right.m_value));
        }

        public static implicit operator TimeSpan(TimeTicks ticks)
        {
            var converter = new Converter { TimeTicks = ticks };

            return converter.TimeSpan;
        }

        public static implicit operator TimeTicks(TimeSpan timeSpan)
        {
            var converter = new Converter { TimeSpan = timeSpan };

            return converter.TimeTicks;
        }

        public override string ToString()
        {
            return AsTimeSpan.ToString("G");
        }
    }
}

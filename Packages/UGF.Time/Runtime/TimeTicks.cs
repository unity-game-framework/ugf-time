using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Time.Runtime
{
    [Serializable]
    public struct TimeTicks : IEquatable<TimeTicks>, IComparable<TimeTicks>
    {
        [SerializeField] private long m_value;

        public long Value { get { return m_value; } }
        public float TotalSeconds { get { return m_value * k_secondsPerTick; } }
        public TimeSpan AsTimeSpan { get { return this; } }

        public static readonly TimeTicks Zero = new TimeTicks(0L);
        public static readonly TimeTicks MinValue = new TimeTicks(long.MinValue);
        public static readonly TimeTicks MaxValue = new TimeTicks(long.MaxValue);

        private const float k_secondsPerTick = 1.0F / TimeSpan.TicksPerSecond;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public TimeSpan TimeSpan;
            [FieldOffset(0)] public TimeTicks TimeTicks;
        }

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

        public static TimeTicks FromSeconds(float seconds)
        {
            return new TimeTicks((long)(seconds * TimeSpan.TicksPerSecond));
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
            return m_value.ToString();
        }
    }
}

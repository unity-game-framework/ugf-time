using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Time.Runtime
{
    [Serializable]
    public struct TimeDate : IEquatable<TimeDate>, IComparable<TimeDate>
    {
        [SerializeField] private ulong m_value;

        public ulong Value { get { return m_value; } }
        public long Ticks { get { return (long)(m_value & k_ticksMask); } }
        public float TotalSeconds { get { return (long)(m_value & k_ticksMask) * SecondsPerTick; } }
        public DateTime AsDateTime { get { return this; } }

        public static TimeDate Now { get { return DateTime.Now; } }
        public static TimeDate UtcNow { get { return DateTime.UtcNow; } }

        public static readonly TimeDate MinValue = new TimeDate(MinTicks);
        public static readonly TimeDate MaxValue = new TimeDate(MaxTicks);

        public const long TicksPerSecond = TimeSpan.TicksPerSecond;
        public const float SecondsPerTick = 1.0F / TicksPerSecond;
        public const long MinTicks = 0L;
        public const long MaxTicks = 3155378975999999999L;

        private const ulong k_ticksMask = 0x3FFFFFFFFFFFFFFF;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public DateTime DateTime;
            [FieldOffset(0)] public TimeDate TimeDate;
        }

        public TimeDate(ulong value)
        {
            m_value = value;
        }

        public bool Equals(TimeDate other)
        {
            return m_value == other.m_value;
        }

        public override bool Equals(object obj)
        {
            return obj is TimeDate other && Equals(other);
        }

        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        public int CompareTo(TimeDate other)
        {
            return m_value.CompareTo(other.m_value);
        }

        public static TimeDate FromTicks(long ticks)
        {
            if (ticks < MinTicks) throw new ArgumentException("The specified ticks can't be less than zero.", nameof(ticks));
            if (ticks > MaxTicks) throw new ArgumentException("The specified ticks can't be greater than max ticks.", nameof(ticks));

            return new TimeDate((ulong)ticks);
        }

        public static TimeDate FromSeconds(float seconds)
        {
            if (seconds < 0F) throw new ArgumentException("The specified seconds can't be less than zero.", nameof(seconds));

            long ticks = (long)(seconds * TicksPerSecond);

            if (ticks > MaxTicks) throw new ArgumentException("The specified seconds can't be greater than max ticks.", nameof(seconds));

            return new TimeDate((ulong)ticks);
        }

        public static bool operator ==(TimeDate left, TimeDate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TimeDate left, TimeDate right)
        {
            return !left.Equals(right);
        }

        public static implicit operator DateTime(TimeDate timeDate)
        {
            var converter = new Converter { TimeDate = timeDate };

            return converter.DateTime;
        }

        public static implicit operator TimeDate(DateTime dateTime)
        {
            var converter = new Converter { DateTime = dateTime };

            return converter.TimeDate;
        }

        public override string ToString()
        {
            return AsDateTime.ToString("O");
        }
    }
}

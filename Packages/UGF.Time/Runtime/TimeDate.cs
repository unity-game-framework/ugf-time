using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Time.Runtime
{
    /// <summary>
    /// Represents serializable date information.
    /// </summary>
    [Serializable]
    public struct TimeDate : IEquatable<TimeDate>, IComparable<TimeDate>
    {
        [SerializeField] private ulong m_value;

        /// <summary>
        /// Gets the raw value of the date information.
        /// </summary>
        public ulong Value { get { return m_value; } }

        /// <summary>
        /// Gets the total ticks of the date and time.
        /// </summary>
        public long Ticks { get { return (long)(m_value & k_ticksMask); } }

        /// <summary>
        /// Gets the total seconds of the date and time.
        /// </summary>
        public float TotalSeconds { get { return (long)(m_value & k_ticksMask) * SecondsPerTick; } }

        /// <summary>
        /// Gets this instance as <see cref="DateTime"/>.
        /// </summary>
        public DateTime AsDateTime { get { return this; } }

        /// <summary>
        /// Gets the current date and time on this application.
        /// </summary>
        public static TimeDate Now { get { return DateTime.Now; } }

        /// <summary>
        /// Gets the current date and time on this application as Coordinated Universal Time (UTC).
        /// </summary>
        public static TimeDate UtcNow { get { return DateTime.UtcNow; } }

        /// <summary>
        /// Represents the smallest possible value of <see cref="TimeDate"/>.
        /// </summary>
        public static readonly TimeDate MinValue = new TimeDate(MinTicks);

        /// <summary>
        /// Represents the largest possible value of <see cref="TimeDate"/>.
        /// </summary>
        public static readonly TimeDate MaxValue = new TimeDate(MaxTicks);

        /// <summary>
        /// Represents the number of ticks per second.
        /// </summary>
        public const long TicksPerSecond = TimeSpan.TicksPerSecond;

        /// <summary>
        /// Represents the number of seconds per tick.
        /// </summary>
        public const float SecondsPerTick = 1.0F / TicksPerSecond;

        /// <summary>
        /// Represents the smallest number of ticks that <see cref="TimeDate"/> can hold.
        /// </summary>
        public const long MinTicks = 0L;

        /// <summary>
        /// Represents the largest number of ticks that <see cref="TimeDate"/> can hold.
        /// </summary>
        public const long MaxTicks = 3155378975999999999L;

        private const ulong k_ticksMask = 0x3FFFFFFFFFFFFFFF;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public DateTime DateTime;
            [FieldOffset(0)] public TimeDate TimeDate;
        }

        /// <summary>
        /// Creates date from the specified raw value.
        /// </summary>
        /// <param name="value">The raw value of the date time.</param>
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

        /// <summary>
        /// Creates date from the specified ticks.
        /// </summary>
        /// <param name="ticks">The total ticks to convert to date.</param>
        public static TimeDate FromTicks(long ticks)
        {
            if (ticks < MinTicks) throw new ArgumentException("The specified ticks can't be less than zero.", nameof(ticks));
            if (ticks > MaxTicks) throw new ArgumentException("The specified ticks can't be greater than max ticks.", nameof(ticks));

            return new TimeDate((ulong)ticks);
        }

        /// <summary>
        /// Creates date from the specified seconds.
        /// </summary>
        /// <param name="seconds">The total seconds to convert to date.</param>
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

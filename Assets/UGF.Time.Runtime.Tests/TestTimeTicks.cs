using NUnit.Framework;

namespace UGF.Time.Runtime.Tests
{
    public class TestTimeTicks
    {
        [Test]
        public void FromSeconds()
        {
            float seconds0 = 0.0015F;
            TimeTicks ticks = TimeTicks.FromSeconds(seconds0);
            float seconds1 = ticks.TotalSeconds;

            Assert.AreEqual(seconds0, seconds1);
        }

        [Test]
        public void FromSecondsOneTick()
        {
            TimeTicks ticks = TimeTicks.FromSeconds(0.0000001F);

            Assert.AreEqual(1L, ticks.Value);
        }

        [Test]
        public void OneTickSeconds()
        {
            TimeTicks ticks = TimeTicks.FromTicks(1L);

            Assert.AreEqual(0.0000001F, ticks.TotalSeconds);
        }
    }
}

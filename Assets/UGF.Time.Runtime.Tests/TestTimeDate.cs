using NUnit.Framework;

namespace UGF.Time.Runtime.Tests
{
    public class TestTimeDate
    {
        [Test]
        public void FromSeconds()
        {
            float seconds0 = 0.00015F;
            TimeDate date = TimeDate.FromSeconds(seconds0);
            float seconds1 = date.TotalSeconds;

            Assert.AreEqual(seconds0, seconds1);
        }

        [Test]
        public void FromSecondsOneTick()
        {
            TimeDate date = TimeDate.FromSeconds(0.0000001F);

            Assert.AreEqual(1, date.Ticks);
        }
    }
}

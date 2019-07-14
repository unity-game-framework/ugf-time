using UGF.Time.Runtime;
using UnityEngine;

namespace UGF.Time.Editor.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDrawersScriptableObject")]
    public class TestDrawersScriptableObject : ScriptableObject
    {
        [SerializeField] private TimeDate m_date;
        [SerializeField] private TimeTicks m_ticks;

        public TimeDate Date { get { return m_date; } set { m_date = value; } }
        public TimeTicks Ticks { get { return m_ticks; } set { m_ticks = value; } }
    }
}

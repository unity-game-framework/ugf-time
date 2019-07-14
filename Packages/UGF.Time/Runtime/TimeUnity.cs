namespace UGF.Time.Runtime
{
    /// <summary>
    /// Represents access to the Unity time.
    /// </summary>
    public class TimeUnity : ITime
    {
        public float Scale { get { return UnityEngine.Time.timeScale; } set { UnityEngine.Time.timeScale = value; } }
        public float Delta { get { return UnityEngine.Time.deltaTime; } }
        public float Elapsed { get { return UnityEngine.Time.time; } }
    }
}

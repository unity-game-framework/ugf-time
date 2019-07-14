namespace UGF.Time.Runtime
{
    /// <summary>
    /// Represents access to time.
    /// </summary>
    public interface ITime
    {
        /// <summary>
        /// Gets or sets the scale of the time.
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Gets the time of the delta.
        /// </summary>
        float Delta { get; }

        /// <summary>
        /// Gets the total time elapsed.
        /// </summary>
        float Elapsed { get; }
    }
}

namespace UGF.Time.Runtime
{
    public interface ITime
    {
        float Scale { get; set; }
        float Delta { get; }
        float Elapsed { get; }
    }
}

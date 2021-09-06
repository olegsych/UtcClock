namespace Chronology
{
    public interface IClock<T>
    {
        T Time { get; }
    }
}

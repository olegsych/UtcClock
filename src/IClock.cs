namespace Chronology
{
    public interface IClock<T>
    {
        T Now { get; }
    }
}

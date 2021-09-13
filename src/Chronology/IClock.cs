namespace Chronology
{
    public interface IClock<out T>
    {
        T Now { get; }
    }
}

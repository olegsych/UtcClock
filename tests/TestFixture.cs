using Fuzzy;

namespace UtcClock
{
    public abstract class TestFixture
    {
        protected static readonly IFuzz fuzzy = new RandomFuzz();
    }
}

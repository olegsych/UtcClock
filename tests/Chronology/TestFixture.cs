using Fuzzy;

namespace Chronology
{
    public abstract class TestFixture
    {
        protected static readonly IFuzz fuzzy = new RandomFuzz();
    }
}

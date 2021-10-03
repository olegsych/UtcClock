using System;
using Chronology;
using Fuzzy.Implementation;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Fuzzy
{
    public class UtcDateTimeExtensionsTest: TestFixture
    {
        // Method parameters
        readonly UtcDateTime value = new UtcDateTime(random.Next());
        readonly UtcDateTime minimum = new UtcDateTime(UtcDateTime.MinValue.Ticks + random.Next());
        readonly TimeSpan timeSpan = new TimeSpan(random.Next());

        // Test fixture
        readonly FuzzyRange<UtcDateTime> spec;
        readonly UtcDateTime newValue = new UtcDateTime(UtcDateTime.MinValue.Ticks + random.Next());

        public UtcDateTimeExtensionsTest() {
            spec = Substitute.ForPartsOf<FuzzyRange<UtcDateTime>>(fuzzy, UtcDateTime.MinValue, UtcDateTime.MaxValue);
            FuzzyContext.Set(value, spec);
            ConfiguredCall arrange = fuzzy.Build(spec).Returns(newValue);
        }

        public class Between: UtcDateTimeExtensionsTest
        {
            [Fact]
            public void ReturnsValueWithMinimumAndMaximumPropertiesSet() {
                UtcDateTime returned = value.Between(minimum, timeSpan);

                Assert.Equal(newValue, returned);
                Assert.Equal(minimum, spec.Minimum);
                Assert.Equal(minimum + timeSpan, spec.Maximum);
            }
        }
    }
}

using System;
using System.Linq;
using Chronology;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public abstract class IServiceCollectionExtensionsTest
    {
        readonly IServiceCollection services = new ServiceCollection();

        public class AddChronology: IServiceCollectionExtensionsTest
        {
            // Act
            public AddChronology() => services.AddChronology();

            [Fact]
            public void RegistersUniversalClockSingleton() =>
                VerifyAddedSingleton<IClock<UtcDateTime>, UniversalClock>(services);

            [Fact]
            public void RegistersLocalClockSingleton() =>
                VerifyAddedSingleton<IClock<DateTimeOffset>, LocalClock>(services);

            static void VerifyAddedSingleton<TService, TImplementation>(IServiceCollection services) {
                ServiceDescriptor descriptor = services.Single(_ => _.ServiceType == typeof(TService));
                Assert.Equal(typeof(TImplementation), descriptor.ImplementationType);
                Assert.Equal(ServiceLifetime.Singleton, descriptor.Lifetime);
            }
        }
    }
}

using System;
using Chronology;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddChronology(this IServiceCollection services) => services
            .AddSingleton<IClock<UtcDateTime>, UniversalClock>()
            .AddSingleton<IClock<DateTimeOffset>, LocalClock>()
            .AddSingleton<IClock<TimeSpan>, HighResolutionClock>();
    }
}

using Hangfire;
using Hangfire.InMemory;
using Hangfire.Logging;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

[CollectionDefinition("HangfireTests")]
public class HangfireTestCollection : ICollectionFixture<HangfireTestFixture>
{
}

public class HangfireTestFixture
{
    private static readonly object _lock = new object();

    public HangfireTestFixture()
    {
        EnsureHangfireConfigured();
    }

    private static void EnsureHangfireConfigured()
    {
        lock (_lock)
        {
            // Always reset LogProvider to null to prevent ObjectDisposedException
            // from previously disposed LoggerFactory (set by integration tests)
            LogProvider.SetCurrentLogProvider(null);
            
            // Configure Hangfire with InMemoryStorage
            GlobalConfiguration.Configuration.UseInMemoryStorage();
        }
    }
}


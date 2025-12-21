using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.RemoveAll(typeof(ApplicationDbContext));
            
            // Add ApplicationDbContext using an in-memory database for testing
            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(_databaseName);
            });

            // Replace GoogleCloudStorageService with MockFileStorageService for testing
            services.RemoveAll(typeof(IFileStorageService));
            services.AddScoped<IFileStorageService, MockFileStorageService>();

            // Build service provider and ensure database is created
            // Note: The seed data comes from ApplicationDbContext.OnModelCreating's HasData
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();

            // EnsureCreated will apply the seed data from HasData in OnModelCreating
            db.Database.EnsureCreated();
        });
    }
}


using HikingTracks.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HikingTracks.Tests;

public class WebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Configure services for the test web host
        builder.ConfigureServices(services =>
        {
            // Replace the DbContext registration with an in-memory database
            this.ReplaceDbContextWithInMemoryDb(services);
        });
    }

    private void ReplaceDbContextWithInMemoryDb(IServiceCollection services)
    {
        // Remove the existing DbContext registration
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<HikingTracksContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        // Add DbContext with an in-memory database
        services.AddDbContext<HikingTracksContext>(options =>
        {
            options.UseInMemoryDatabase(_dbName);
        });
    }
}
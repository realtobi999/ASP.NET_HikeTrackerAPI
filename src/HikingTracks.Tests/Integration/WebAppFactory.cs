using HikingTracks.Infrastructure;
using HikingTracks.Presentation;
using HikingTracks.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            ReplaceDbContextWithInMemoryDb(services);

            // Remove middleware here
            RemoveMiddleware<AccountAuthenticationMiddleware>(services);
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

    private void RemoveMiddleware<TMiddleware>(IServiceCollection services)
    {
        var middlewareDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(TMiddleware));

        if (middlewareDescriptor != null)
        {
            services.Remove(middlewareDescriptor);
        }
    }
}

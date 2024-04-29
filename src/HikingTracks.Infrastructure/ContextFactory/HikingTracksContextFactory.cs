using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HikingTracks.Infrastructure.ContextFactory;

public class HikingTracksContextFactory : IDesignTimeDbContextFactory<HikingTracksContext>
{
    public HikingTracksContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<HikingTracksContext>().UseNpgsql(
            "Host=localhost;Username=postgres;Password=root;Database=HikingTracks", b => b.MigrationsAssembly("HikingTracks.Infrastructure"));

        return new HikingTracksContext(builder.Options);
    }
}


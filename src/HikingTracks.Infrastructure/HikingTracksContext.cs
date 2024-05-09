using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure;

public class HikingTracksContext(DbContextOptions<HikingTracksContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Hike> Hikes { get; set; }
    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
        .HasIndex(e => e.Email)
        .IsUnique();

        modelBuilder.Entity<Photo>()
        .HasOne(p => p.Hike)
        .WithMany(h => h.Photos)
        .HasForeignKey(p => p.HikeID);
    }
}

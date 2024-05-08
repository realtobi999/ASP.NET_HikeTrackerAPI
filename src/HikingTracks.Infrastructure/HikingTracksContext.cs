using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure;

public class HikingTracksContext(DbContextOptions<HikingTracksContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Hike> Hikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
        .HasIndex(e => e.Email)
        .IsUnique();

        modelBuilder.Entity<Hike>()
        .HasOne(a => a.Account)
        .WithMany(h => h.Hikes)
        .HasForeignKey(h => h.AccountId);

        modelBuilder.Entity<Photo>()
        .HasOne(p => p.Hike)
        .WithMany(h => h.Photos)
        .HasForeignKey(p => p.HikeID);
    }
}

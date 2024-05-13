using HikingTracks.Domain;
using HikingTracks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure;

public class HikingTracksContext(DbContextOptions<HikingTracksContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Hike> Hikes { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Segment> Segments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<Photo>()
            .HasOne(p => p.Hike)
            .WithMany(h => h.Photos)
            .HasForeignKey(p => p.HikeID);

        // Configure many-to-many relationship between Segment and Hike
        modelBuilder.Entity<SegmentHike>()
            .HasKey(sh => new { sh.SegmentId, sh.HikeId });

        modelBuilder.Entity<SegmentHike>()
            .HasOne(sh => sh.Segment)
            .WithMany(s => s.SegmentHike)
            .HasForeignKey(sh => sh.SegmentId);

        modelBuilder.Entity<SegmentHike>()
            .HasOne(sh => sh.Hike)
            .WithMany(h => h.SegmentHike)
            .HasForeignKey(sh => sh.HikeId);
    }
}

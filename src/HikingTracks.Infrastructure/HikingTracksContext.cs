using HikingTracks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HikingTracks.Infrastructure;

public class HikingTracksContext(DbContextOptions<HikingTracksContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
}

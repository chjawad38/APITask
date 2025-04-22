using BeerCollectionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerCollectionAPI.Data;

public class BeerCollectionContext : DbContext
{
    public BeerCollectionContext(DbContextOptions<BeerCollectionContext> options)
        : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; }
    public DbSet<BeerRating> BeerRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>()
            .HasMany(b => b.Ratings)
            .WithOne(r => r.Beer)
            .HasForeignKey(r => r.BeerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
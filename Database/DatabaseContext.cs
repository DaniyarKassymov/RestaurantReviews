using exam9kassymovdaniyar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace exam9kassymovdaniyar.Database;

public class DatabaseContext : IdentityDbContext<User>
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Restaurant>()
            .HasMany(r => r.Reviews)
            .WithOne(c => c.Restaurant)
            .HasForeignKey(c => c.RestaurantId);
        
        builder.Entity<User>()
            .HasMany(r => r.Reviews)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);
        
        base.OnModelCreating(builder);
    }
}
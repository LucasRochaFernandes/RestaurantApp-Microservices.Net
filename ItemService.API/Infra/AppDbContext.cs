using ItemService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemService.API.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<RestaurantReference> RestaurantsReference { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
       .HasOne(i => i.RestaurantReference) 
       .WithMany(r => r.Items) 
       .HasForeignKey(i => i.RestaurantId) 
       .OnDelete(DeleteBehavior.Restrict); 
    }
}

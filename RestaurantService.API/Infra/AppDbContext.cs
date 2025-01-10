
using Microsoft.EntityFrameworkCore;
using RestaurantService.API.Domain.Entities;

namespace RestaurantService.API.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}

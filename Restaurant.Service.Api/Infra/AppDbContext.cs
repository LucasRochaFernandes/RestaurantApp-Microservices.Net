
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Service.Api.Infra;

public class AppDbContext : DbContext
{
    //public DbSet<Restaurante> Restaurantes { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}

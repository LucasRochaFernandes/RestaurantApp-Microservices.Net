using ItemService.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemService.API.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }
}

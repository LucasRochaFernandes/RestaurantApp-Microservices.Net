using ItemService.API.Domain.Entities;
using ItemService.API.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ItemService.API.Infra.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _dbContext;

    public ItemRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<Item> Create(Item entity)
    {
        var result = await _dbContext.Items.AddAsync(entity);
        return result.Entity;
    }

    public async Task<Item?> GetItemById(Guid id)
    {
        var entity = await _dbContext.Items.FirstOrDefaultAsync(it => it.Id.Equals(id));
        return entity;
    }

    public async Task<IList<Item>> GetItemsByRestaurantId(Guid restauranteId)
    {
        var entity = await _dbContext.Items.Where(it => it.RestaurantId.Equals(restauranteId)).ToListAsync();
        return entity;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}

using ItemService.API.Domain.Entities;

namespace ItemService.API.Domain.IRepositories;

public interface IItemRepository
{
    public Task<Item> Create(Item entity);
    public Task<IList<Item>> GetItemsByRestaurantId(Guid restauranteId);
    public Task<Item?> GetItemById(Guid id);
    public Task SaveChanges();
}

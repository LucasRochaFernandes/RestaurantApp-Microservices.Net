using ItemService.API.Domain.Entities;

namespace ItemService.API.Domain.IRepositories;

public interface IRestaurantReferenceRepository
{
    public Task<RestaurantReference?> GetById(Guid id);
    public Task<RestaurantReference> Create(RestaurantReference entity);
    public Task SaveChanges();
}

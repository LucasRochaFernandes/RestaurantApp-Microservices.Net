
using RestaurantService.API.Domain.Entities;

namespace RestaurantService.API.Domain.Repositories;

public interface IRestaurantRepository
{
    public Task<Restaurant> Create(Restaurant restaurant);
    public Task<IList<Restaurant>> GetAll();
    public Task<Restaurant?> GetRestaurantById(Guid id);
    public Task SaveChanges();
}

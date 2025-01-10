using Microsoft.EntityFrameworkCore;
using RestaurantService.API.Domain.Entities;
using RestaurantService.API.Domain.Repositories;

namespace RestaurantService.API.Infra.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _dbContext;

    public RestaurantRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Restaurant> Create(Restaurant restaurant)
    {
       var result = await _dbContext.Restaurants.AddAsync(restaurant);
       return result.Entity;
    }

    public async Task<IList<Restaurant>> GetAll()
    {
        var result = await _dbContext.Restaurants.ToListAsync();
        return result;
    }

    public async Task<Restaurant?> GetRestaurantById(Guid id)
    {
        var entity = await _dbContext.Restaurants.FirstOrDefaultAsync(rt => rt.Id.Equals(id));
        return entity;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}

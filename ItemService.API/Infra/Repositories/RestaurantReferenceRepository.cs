using AutoMapper;
using ItemService.API.Domain.Entities;
using ItemService.API.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ItemService.API.Infra.Repositories;

public class RestaurantReferenceRepository : IRestaurantReferenceRepository
{
    private readonly AppDbContext _dbContext;
    public RestaurantReferenceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<RestaurantReference> Create(RestaurantReference entity)
    {
        var result = await _dbContext.RestaurantsReference.AddAsync(entity);
        return result.Entity;
    }
    public async Task<RestaurantReference?> GetById(Guid id)
    {
        var entity = await _dbContext.RestaurantsReference.FirstOrDefaultAsync(rf => rf.Id.Equals(id));
        return entity;
    }
    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}

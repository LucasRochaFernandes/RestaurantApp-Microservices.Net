
namespace Restaurant.Service.Api.Domain.Repositories;

public interface IRestaurantRepository
{
    public void SaveChanges();
    public IEnumerable<Restaurant> GetAllRestaurantes();
    public Restaurant GetRestauranteById(int id);
    void CreateRestaurante(Restaurant restaurante);
}

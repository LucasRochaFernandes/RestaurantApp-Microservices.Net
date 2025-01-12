using System.ComponentModel.DataAnnotations;

namespace ItemService.API.Domain.Entities;

public class RestaurantReference
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Item> Items {  get; set; }
}

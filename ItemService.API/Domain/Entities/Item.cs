using System.ComponentModel.DataAnnotations;

namespace ItemService.API.Domain.Entities;

public class Item
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public Guid RestaurantId { get; set; }
}

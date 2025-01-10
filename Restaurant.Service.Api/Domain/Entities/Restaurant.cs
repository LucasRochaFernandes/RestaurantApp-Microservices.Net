using System.ComponentModel.DataAnnotations;

namespace Restaurant.Service.Api.Domain.Entities;

public class Restaurant
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string SiteURL { get; set; }

}

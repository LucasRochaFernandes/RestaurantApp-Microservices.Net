using ItemService.API.Application.UseCases;
using ItemService.API.Communication.Requests;
using ItemService.API.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemService.API.Controllers;
[Route("api/Item/[controller]")]
[ApiController]
public class RestaurantReferenceController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] RegisterRestaurantReferenceUseCase useCase,
        [FromBody] RestaurantReferenceJsonRequest body)
    {
        var result = await useCase.Execute(body);
        return Created(string.Empty, result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext dbContext)
    {
        var result = await dbContext.RestaurantsReference.ToListAsync();
        return Ok(result);
    }
}

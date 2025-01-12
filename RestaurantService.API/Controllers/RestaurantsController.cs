using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantService.API.Application.UseCases;
using RestaurantService.API.Communication.Requests;
using RestaurantService.API.Communication.Responses;
using RestaurantService.API.ExternalServices.ItemService;
using RestaurantService.API.ExternalServices.RabbitMQ;
using RestaurantService.API.Infra;

namespace RestaurantService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(RestaurantJsonResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] RegisterRestaurantUseCase useCase, 
        [FromServices] IRabbitMQClient rabbitMQClient,
        [FromBody] RestaurantJsonRequest body)
    {
        var result = await useCase.Execute(body);
        await rabbitMQClient.PubRestaurantReference(result);
        return Created(string.Empty, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] AppDbContext dbContext)
    {
        var result = await dbContext.Restaurants.ToListAsync();
        return Ok(result);
    }
}

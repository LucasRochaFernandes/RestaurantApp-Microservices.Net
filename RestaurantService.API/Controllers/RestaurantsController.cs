using Microsoft.AspNetCore.Mvc;
using RestaurantService.API.Application.UseCases;
using RestaurantService.API.Communication.Requests;
using RestaurantService.API.Communication.Responses;

namespace RestaurantService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RestaurantJsonResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] RegisterRestaurantUseCase useCase, 
        [FromBody] RestaurantJsonRequest body)
    {
        var result = await useCase.Execute(body);
        return Created(string.Empty, result);
    }
}

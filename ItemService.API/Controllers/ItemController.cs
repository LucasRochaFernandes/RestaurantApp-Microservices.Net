using ItemService.API.Application.UseCases;
using ItemService.API.Communication.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] ItemJsonRequest body, 
        [FromServices] RegisterItemUseCase useCase)
    {
        var result = await useCase.Execute(body);
        return Created(string.Empty, result);
    } 
}

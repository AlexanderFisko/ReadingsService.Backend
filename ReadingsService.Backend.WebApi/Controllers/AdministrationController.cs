using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingsService.Backend.Shared.Commands;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.WebApi.Controllers;

[ApiController]
[Route("/[action]")]
public class AdministrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdministrationController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto<string>>> ClearAsync(CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(new AdministrationClearRequest(), cancellationToken));
}
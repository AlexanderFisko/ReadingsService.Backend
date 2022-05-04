using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingsService.Backend.Shared.Commands;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SequenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public SequenceController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto<SequenceCreateResponseDto>>> CreateAsync(CancellationToken cancellationToken) =>
        Ok(await _mediator.Send(new SequenceCreateRequest(), cancellationToken));
}
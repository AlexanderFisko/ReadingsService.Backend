using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingsService.Backend.Shared;
using ReadingsService.Backend.Shared.Commands;
using ReadingsService.Backend.Shared.Models.Requests;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using ReadingsService.Backend.WebApi.Validators;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ObservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ObservationController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseDto<ObservationAddResponseDto>>> AddAsync([FromBody] ObservationAddRequestDto data, CancellationToken cancellationToken)
    {
        var observationAddRequestValidator = new ObservationAddRequestValidator();

        var validationResult = await observationAddRequestValidator.ValidateAsync(data, cancellationToken);
        if (validationResult.IsValid)
            return Ok(await _mediator.Send(new ObservationAddRequest(data), cancellationToken));

        var result = new ResponseDto<ObservationAddResponseDto>
        {
            Status = ResponseStatus.Error,
            Msg = validationResult.ToString()
        };

        return Ok(result);
    }
}
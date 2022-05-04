using MediatR;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.Shared.Commands;

internal class SequenceCreateRequestHandler : IRequestHandler<SequenceCreateRequest, ResponseDto<SequenceCreateResponseDto>>
{
    private readonly ISequenceService _sequenceService;

    public SequenceCreateRequestHandler(ISequenceService sequenceService) => _sequenceService = sequenceService;

    public async Task<ResponseDto<SequenceCreateResponseDto>> Handle(SequenceCreateRequest request, CancellationToken cancellationToken)
    {
        var id = await _sequenceService.CreateAsync(cancellationToken);
        return new ResponseDto<SequenceCreateResponseDto>
        {
            Response = new SequenceCreateResponseDto(id)
        };
    }
}
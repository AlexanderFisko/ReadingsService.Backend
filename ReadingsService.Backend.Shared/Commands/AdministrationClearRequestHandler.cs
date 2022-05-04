using MediatR;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.Shared.Commands;

internal class AdministrationClearRequestHandler : IRequestHandler<AdministrationClearRequest, ResponseDto<string>>
{
    private readonly ISequenceService _sequenceService;

    public AdministrationClearRequestHandler(ISequenceService sequenceService) => _sequenceService = sequenceService;

    public async Task<ResponseDto<string>> Handle(AdministrationClearRequest request, CancellationToken cancellationToken)
    {
        await _sequenceService.ClearAsync(cancellationToken);

        return new ResponseDto<string>
        {
            Response = "Ok"
        };
    }
}
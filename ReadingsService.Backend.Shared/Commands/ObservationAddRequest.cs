using MediatR;
using ReadingsService.Backend.Shared.Models.Requests;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;

namespace ReadingsService.Backend.Shared.Commands;

public class ObservationAddRequest : IRequest<ResponseDto<ObservationAddResponseDto>>
{
    public ObservationAddRequest(ObservationAddRequestDto data) => Data = data;

    public ObservationAddRequestDto Data { get; }
}
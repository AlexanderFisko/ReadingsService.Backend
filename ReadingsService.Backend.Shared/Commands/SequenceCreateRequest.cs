using MediatR;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;

namespace ReadingsService.Backend.Shared.Commands;

public class SequenceCreateRequest : IRequest<ResponseDto<SequenceCreateResponseDto>> {}
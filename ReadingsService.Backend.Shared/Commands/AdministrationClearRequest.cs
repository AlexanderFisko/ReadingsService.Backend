using MediatR;
using ReadingsService.Backend.Shared.Models.Responses.Base;

namespace ReadingsService.Backend.Shared.Commands;

public class AdministrationClearRequest : IRequest<ResponseDto<string>> {}
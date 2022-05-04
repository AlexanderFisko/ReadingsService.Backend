using FluentValidation;
using ReadingsService.Backend.Shared.Models.Requests;

namespace ReadingsService.Backend.WebApi.Validators;

internal class ObservationAddRequestValidator : AbstractValidator<ObservationAddRequestDto>
{
    public ObservationAddRequestValidator() => RuleFor(x => x.Observation).SetValidator(new ObservationRequestValidator());
}
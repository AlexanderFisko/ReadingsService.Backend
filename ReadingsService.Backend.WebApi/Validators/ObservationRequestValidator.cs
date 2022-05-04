using FluentValidation;
using ReadingsService.Backend.Shared.Models.Requests;
using System.Linq;

namespace ReadingsService.Backend.WebApi.Validators;

internal class ObservationRequestValidator : AbstractValidator<ObservationRequestDto>
{
    public ObservationRequestValidator()
    {
        const int patternLength = 7;
        const int displayCount = 2;

        RuleFor(x => x.Numbers)
            .Must(x => x.Count() == displayCount)
            .WithMessage(m => $"{nameof(m.Numbers)} should contain two elements")
            .Must(x => x.All(n => n.Length == patternLength && n.All(s => s == '0' || s == '1')))
            .WithMessage(m => $"{nameof(m.Numbers)} must contain only '0' and '1' and have a length equal {patternLength:0}");
    }
}
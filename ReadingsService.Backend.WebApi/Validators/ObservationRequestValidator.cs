using FluentValidation;
using ReadingsService.Backend.Core;
using ReadingsService.Backend.Shared.Models.Requests;
using System.Linq;

namespace ReadingsService.Backend.WebApi.Validators;

internal class ObservationRequestValidator : AbstractValidator<ObservationRequestDto>
{
    public ObservationRequestValidator()
    {
        const int patternLength = SevenSegmentDisplay.PatternLength;

        RuleFor(x => x.Numbers)
            .Must(x => x == null || x.All(n => n.Length == patternLength && n.All(s => s == '0' || s == '1')))
            .WithMessage(m => $"{nameof(m.Numbers)} must contain only '0' and '1' and have a length equal {patternLength:0}");
    }
}
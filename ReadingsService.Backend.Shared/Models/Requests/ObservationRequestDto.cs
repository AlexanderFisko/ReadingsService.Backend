using ReadingsService.Backend.Core;
using System.Collections.Generic;

namespace ReadingsService.Backend.Shared.Models.Requests;

public class ObservationRequestDto
{
    public Color Color { get; set; }

    public IEnumerable<string>? Numbers { get; set; }
}
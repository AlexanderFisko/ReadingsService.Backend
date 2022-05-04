using System;

namespace ReadingsService.Backend.Shared.Models.Requests;

public class ObservationAddRequestDto
{
    public ObservationRequestDto Observation { get; set; } = null!;

    public Guid Sequence { get; set; }
}
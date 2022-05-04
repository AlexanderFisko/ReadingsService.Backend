using System.Collections.Generic;

namespace ReadingsService.Backend.Shared.Models.Responses;

public class ObservationAddResponseDto
{
    public ObservationAddResponseDto(IEnumerable<byte> start, IEnumerable<string> missing)
    {
        Start = start;
        Missing = missing;
    }

    public IEnumerable<byte> Start { get; }

    public IEnumerable<string> Missing { get; }
}
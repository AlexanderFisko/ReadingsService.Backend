using System;

namespace ReadingsService.Backend.Shared.Models.Responses;

public class SequenceCreateResponseDto
{
    public SequenceCreateResponseDto(Guid sequence) => Sequence = sequence;

    public Guid Sequence { get; }
}
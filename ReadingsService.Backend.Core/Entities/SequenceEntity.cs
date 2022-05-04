using System;
using System.Collections.Generic;

namespace ReadingsService.Backend.Core.Entities;

public class SequenceEntity
{
    public Guid Id { get; set; }

    public ICollection<ObservationEntity>? Observations { get; set; }
}
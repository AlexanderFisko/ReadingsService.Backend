using ReadingsService.Backend.Core;
using ReadingsService.Backend.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.Shared;

public interface ISequenceService
{
    Task<Guid> CreateAsync(CancellationToken cancellationToken);

    Task ClearAsync(CancellationToken cancellationToken);

    Task<Sequence?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddObservationByIdAsync(Guid id, Color color, byte? displayFirst, byte? displaySecond, CancellationToken cancellationToken);
}
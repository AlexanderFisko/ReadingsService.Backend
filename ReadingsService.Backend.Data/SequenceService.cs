using Microsoft.EntityFrameworkCore;
using ReadingsService.Backend.Core;
using ReadingsService.Backend.Core.Entities;
using ReadingsService.Backend.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.Data;

public class SequenceService : ISequenceService
{
    private readonly ApplicationDbContext _dbContext;

    public SequenceService(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> CreateAsync(CancellationToken cancellationToken)
    {
        var sequence = new SequenceEntity();
        _dbContext.Sequences.Add(sequence);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return sequence.Id;
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
        var idsList = await _dbContext.Sequences.Select(x => x.Id).ToListAsync(cancellationToken);
        if (!idsList.Any())
            return;

        foreach (var sequence in idsList.Select(id => new SequenceEntity { Id = id }))
        {
            _dbContext.Sequences.Attach(sequence);
            _dbContext.Sequences.Remove(sequence);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SequenceEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _dbContext.Sequences.Include(x => x.Observations)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddObservationByIdAsync(Guid id, Color color, byte displayLow, byte displayHigh, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Sequences.AnyAsync(x => x.Id == id, cancellationToken))
            throw new Exception($"{nameof(SequenceEntity)} with {nameof(SequenceEntity.Id)} = \"{id}\" does not exists");

        var observation = new ObservationEntity
        {
            SequenceId = id,
            Color = color,
            DisplayLow = displayLow,
            DisplayHigh = displayHigh
        };

        _dbContext.Observations.Add(observation);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
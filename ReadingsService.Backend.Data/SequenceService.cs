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
        var sequence = new Sequence();
        _dbContext.Sequences.Add(sequence);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return sequence.Id;
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
        var idsList = await _dbContext.Sequences.Select(x => x.Id).ToListAsync(cancellationToken);
        if (!idsList.Any())
            return;

        foreach (var sequence in idsList.Select(id => new Sequence { Id = id }))
        {
            _dbContext.Sequences.Attach(sequence);
            _dbContext.Sequences.Remove(sequence);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Sequence?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await _dbContext.Sequences.Include(x => x.Observations)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddObservationByIdAsync(Guid id, Color color, byte? displayFirst, byte? displaySecond, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Sequences.AnyAsync(x => x.Id == id, cancellationToken))
            throw new Exception($"{nameof(Sequence)} with {nameof(Sequence.Id)} = \"{id}\" does not exists");

        var observation = new Observation
        {
            SequenceId = id,
            Color = color,
            FirstDisplay = displayFirst,
            SecondDisplay = displaySecond
        };

        _dbContext.Observations.Add(observation);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
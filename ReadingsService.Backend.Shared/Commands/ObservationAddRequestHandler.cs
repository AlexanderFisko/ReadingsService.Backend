using MediatR;
using ReadingsService.Backend.Core;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingsService.Backend.Shared.Commands;

internal class ObservationAddRequestHandler : IRequestHandler<ObservationAddRequest, ResponseDto<ObservationAddResponseDto>>
{
    private readonly ISequenceService _sequenceService;

    public ObservationAddRequestHandler(ISequenceService sequenceService) => _sequenceService = sequenceService;

    public async Task<ResponseDto<ObservationAddResponseDto>> Handle(ObservationAddRequest request, CancellationToken cancellationToken)
    {
        var data = request.Data;
        var id = data.Sequence;
        var color = data.Observation.Color;

        // // Получаем всю сущность целиком, с потенциально ненужными полями. Весьма сомнительно, но и так сойдет.
        var sequence = await _sequenceService.GetByIdAsync(id, cancellationToken);
        if (sequence is null)
            return new ResponseDto<ObservationAddResponseDto>
            {
                Status = ResponseStatus.Error,
                Msg = "The sequence isn't found"
            };

        if (color == Color.Red && sequence.Observations?.Any() != true)
            return new ResponseDto<ObservationAddResponseDto>
            {
                Status = ResponseStatus.Error,
                Msg = "There isn't enough data"
            };

        if (sequence.Observations?.Any(x => x.Color == Color.Red) == true)
            return new ResponseDto<ObservationAddResponseDto>
            {
                Status = ResponseStatus.Error,
                Msg = "“The red observation should be the last"
            };

        var isLast = data.Observation.Color == Color.Red;

        var numbers = data.Observation.Numbers?.ToArray();
        if (!isLast && numbers?.Length != 2)
            throw new Exception("Only two numbers allowed");

        var values = sequence.Observations?.OrderBy(x => x.Id)
                         .Select(x => (First: new BitArray(new[] { x.FirstDisplay!.Value }), Second: new BitArray(new[] { x.SecondDisplay!.Value })))
                         .ToList()
                     ?? new List<(BitArray First, BitArray Second)>();

        if (!isLast)
            values.Add((SevenSegmentDisplay.GetFromString(numbers![1]), SevenSegmentDisplay.GetFromString(numbers[0])));

        // var startValue = values.First();
        // var start = new List<byte>();
        // foreach (var x in SevenSegmentDisplay.GetPossibleValuesByPattern(startValue.First))
        // foreach (var y in SevenSegmentDisplay.GetPossibleValuesByPattern(startValue.Second))
        //     start.Add((byte)(10 * x + y));

        // var missing = new List<BitArray>();

        foreach (var item in values)
        {
            // TODO: реализовать логику анализа последовательности :)
        }


        // TODO: write
        await _sequenceService.AddObservationByIdAsync(id, color, 0, 0, cancellationToken);

        return new ResponseDto<ObservationAddResponseDto>
        {
            // TODO: result
            Response = new ObservationAddResponseDto(Array.Empty<byte>(), Array.Empty<string>())
        };
    }
}
using MediatR;
using ReadingsService.Backend.Core;
using ReadingsService.Backend.Shared.Models.Responses;
using ReadingsService.Backend.Shared.Models.Responses.Base;
using System;
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


        var numbers = data.Observation.Numbers.ToArray();
        if (numbers.Length != 2)
            throw new Exception("Only two numbers allowed");

        var lowNumber = GetFromString(numbers[1]);
        var highNumber = GetFromString(numbers[0]);

        // TODO: реализовать логику анализа последовательности :)
        await _sequenceService.AddObservationByIdAsync(id, color, 0, 0, cancellationToken);

        return new ResponseDto<ObservationAddResponseDto>
        {
            Response = new ObservationAddResponseDto(Array.Empty<byte>(), Array.Empty<string>())
        };
    }

    private static bool[] GetFromString(string value)
    {
        const int patternLength = 7;

        if (value.Length != patternLength)
            throw new ArgumentException("Invalid value", nameof(value));

        var result = new bool[patternLength];

        for (var i = 0; i < patternLength; i++)
        {
            var chr = value[i];
            if (chr != '0' && chr != '1')
                throw new ArgumentException("Invalid value", nameof(value));

            result[i] = chr == '1';
        }

        return result;
    }
}
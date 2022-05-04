namespace ReadingsService.Backend.Shared.Models.Responses.Base;

public class ResponseDto<T>
{
    public ResponseStatus Status { get; init; }

    public string? Msg { get; init; } // TODO: не сериализовать null

    public T? Response { get; init; }
}
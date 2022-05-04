﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReadingsService.Backend.WebApi;

internal class JsonGuidConverter : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => Guid.ParseExact(reader.GetString()!, "N");

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("N"));
}
using Microsoft.AspNetCore.Routing;
using System;
using System.Text.RegularExpressions;

namespace ReadingsService.Backend.WebApi.AppStart;

/// <summary>
/// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-6.0#use-a-parameter-transformer-to-customize-token-replacement
/// </summary>
internal class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value is null
        ? null
        : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2", RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
}
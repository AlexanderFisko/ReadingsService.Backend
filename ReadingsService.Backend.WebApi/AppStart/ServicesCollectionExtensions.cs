using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ReadingsService.Backend.WebApi.AppStart;

internal static class ServicesCollectionExtensions
{
    public static void ConfigureRoutes(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(routeOptions =>
        {
            routeOptions.LowercaseQueryStrings = true;
            routeOptions.LowercaseUrls = true;
        });

        services.Configure<MvcOptions>(mvcOptions =>
        {
            mvcOptions.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });
    }
}
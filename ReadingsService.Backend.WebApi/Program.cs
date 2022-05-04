using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReadingsService.Backend.Data;
using ReadingsService.Backend.Shared;
using ReadingsService.Backend.Shared.Commands;
using ReadingsService.Backend.WebApi;
using ReadingsService.Backend.WebApi.AppStart;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers().AddJsonOptions(jsonOptions =>
{
    var jsonConverters = jsonOptions.JsonSerializerOptions.Converters;
    jsonConverters.Add(new JsonStringEnumConverter());
    jsonConverters.Add(new JsonGuidConverter());
});
services.ConfigureRoutes();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(typeof(AdministrationClearRequest));
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=test.db")); // TODO: в конфигурацию
services.AddScoped<ISequenceService, SequenceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
    context.Database.EnsureCreated();

app.Run();
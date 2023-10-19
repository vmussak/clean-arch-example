using CleanArchExample.Bootstrap.DependencyInjection;
using CleanArchExample.Bootstrap.DependencyInjection.Repositories;
using CleanArchExample.Bootstrap.DependencyInjection.UseCases;
using HealthChecks.UI.Client;
using CleanArchExample.Bootstrap.HealthCheck;
using CleanArchExample.Bootstrap.Versioning;
using CleanArchExample.Bootstrap.Swagger;
using CleanArchExample.Api.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using CleanArchExample.Bootstrap.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddMongoDbRepositories();

builder.Services.AddCustomerSuitabilityUseCases();

builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddHealthChecks().AddMonitoredItem(builder.Configuration);
builder.Services.AddCustomVersioning();
builder.Services.AddCustomSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCustomVersioning();

var provider = builder.Services?.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
app.UseCustomSwagger(provider);

var loggerFactory = builder.Services?.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
app.UseCustomLog(loggerFactory);

app.UseCors(op =>
{
    op.AllowAnyOrigin();
    op.AllowAnyMethod();
    op.AllowAnyHeader();
});

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});

app.Run();

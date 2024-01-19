using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace CleanArchExample.Bootstrap.Logging
{
    public static class LoggingConfiguration
    {
        public static void UseCustomLog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithCorrelationId()
    .Enrich.WithProperty("ApplicationName", "Cargo Fidc")
    .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
    .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
    .WriteTo.Async(wt => wt.MongoDB(configuration.GetConnectionString("MongoDbLogs"), collectionName: "CargoFidcLogs"))
    .CreateLogger();

loggerFactory.AddSerilog(Log.Logger);
        }
    }
}

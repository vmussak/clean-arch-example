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
                .Enrich.FromLogContext()
                .CreateLogger();

            loggerFactory.AddSerilog(Log.Logger);
        }
    }
}

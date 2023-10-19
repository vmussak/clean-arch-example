using CleanArchExample.Infra.Data.MongoDb.Context;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Bootstrap.DependencyInjection
{
    public static class MongoDbDependencyInjection
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbConnectionString = configuration.GetConnectionString("MongoDb");

            services.AddScoped<IMongoContext>(x => new MongoContext(new MongoClient(mongoDbConnectionString), "BeyondSuitability"));

            return services;
        }
    }
}

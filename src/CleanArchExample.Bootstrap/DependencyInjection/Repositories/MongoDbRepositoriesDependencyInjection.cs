using CleanArchExample.Application.Repositories;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create;
using CleanArchExample.Infra.Data.MongoDb.Context;
using CleanArchExample.Infra.Data.MongoDb.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Bootstrap.DependencyInjection.Repositories
{
    public static class MongoDbRepositoriesDependencyInjection
    {
        public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerSuitabilityRepository, CustomerSuitabilityRepository>();

            return services;
        }
    }
}

using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Create;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Delete;
using CleanArchExample.Application.UseCases.CustomerSuitabilityUseCases.Get;
using CleanArchExample.Infra.Data.MongoDb.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Bootstrap.DependencyInjection.UseCases
{
    public static class CustomerSuitabilityUseCasesDependencyInjection
    {
        public static IServiceCollection AddCustomerSuitabilityUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICustomerSuitabilityCreateUseCase, CustomerSuitabilityCreateUseCase>();
            services.AddScoped<ICustomerSuitabilityGetUseCases, CustomerSuitabilityGetUseCases>();
            services.AddScoped<ICustomerSuitabilityDeleteUseCase, CustomerSuitabilityDeleteUseCase>();

            return services;
        }
    }
}

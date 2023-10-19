using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchExample.Bootstrap.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Suitability",
                    Description = "Responsável por gerenciar Suitabilities",
                    Contact = new OpenApiContact
                    {
                        Name = "Beyond",
                        Email = "vinicius.mussak@imaginebeyond.com.br"
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(setup =>
            {
                setup.PreSerializeFilters.Add((document, request) =>
                    document.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"https://{request.Host.Value}"
                        }
                    }
                );
            }).UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = "swagger";
                setup.OAuthAppName("Suitability Swagger UI");

                foreach (var d in provider.ApiVersionDescriptions)
                    setup.SwaggerEndpoint($"./{d.GroupName}/swagger.json", $"{d.GroupName}");
            });

            return app;
        }
    }
}


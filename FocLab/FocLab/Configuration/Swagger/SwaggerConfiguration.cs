using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace FocLab.Configuration.Swagger
{
    public class SwaggerConfiguration
    {
        public static void ConfigureSwagger(IServiceCollection services, List<string> xmlDocs)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                c.SchemaFilter<EnumSchemaFilter>();
                c.EnableAnnotations();
                xmlDocs.ForEach(x => c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, x)));
            });
        }
    }
}

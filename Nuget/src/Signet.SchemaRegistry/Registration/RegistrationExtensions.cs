using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Signet.SchemaRegistry.Registration
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddSignetSchemaRegistry(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<SchemaRegistrySettings>(
                builder.Configuration.GetSection(nameof(SchemaRegistrySettings))
            );

            builder.Services.AddHttpClient("SignetClient", (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<SchemaRegistrySettings>>().Value;

                if (!string.IsNullOrEmpty(options.BaseUrl))
                {
                    client.BaseAddress = new Uri(options.BaseUrl);
                }

                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return builder.Services;
        }
    }
}

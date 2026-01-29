using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;
using Signet.Api.Features.Schemas.Endpoints.GetSchemaById;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Repositories;
using Signet.Api.Features.Schemas.UseCases.AddSchema;
using Signet.Api.Features.Schemas.UseCases.GetSchemaById;

namespace Signet.Api.Features.Schemas.Extensions
{
    public static class SchemaFeatureExtensions
    {
        public static WebApplicationBuilder AddSchemaUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUseCaseVoid<AddSchemaInputDto>, AddSchemaUseCase>();

            builder.Services.AddScoped<IUseCase<string, IEnumerable<GetSchemasByIdEndpointResponseDto>>, GetSchemasByIdUseCase>();

            return builder;
        }

        public static WebApplicationBuilder AddSchemaInfrastructureExtensions(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISchemaRepository, SchemaRepository>();

            return builder;
        }
    }
}

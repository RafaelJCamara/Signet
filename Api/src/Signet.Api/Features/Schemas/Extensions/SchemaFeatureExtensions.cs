using Signet.Api.Common.UseCases;
using Signet.Api.Domain.Entities;
using Signet.Api.Domain.Repositories;
using Signet.Api.Features.Containers.Common.Infrastructure;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;
using Signet.Api.Features.Schemas.UseCases.AddSchema;

namespace Signet.Api.Features.Schemas.Extensions;

public static class SchemaFeatureExtensions
{
    public static WebApplicationBuilder AddSchemaUseCases(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUseCaseVoid<AddSchemaInputDto>, AddSchemaUseCase>();

        //builder.Services.AddScoped<IUseCase<GetSchemasByIdEndpointRequestDto, IEnumerable<GetSchemasByIdEndpointResponseDto>>, GetSchemasByIdUseCase>();

        //builder.Services.AddScoped<IUseCaseOutputOnly<IEnumerable<GetAllSchemasEndpointResponseDto>>, GetAllSchemasUseCase>();

        return builder;
    }

    public static WebApplicationBuilder AddSchemaInfrastructureExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository<SchemaContainer>, SchemaContainerRepository>();

        return builder;
    }
}

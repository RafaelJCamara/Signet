using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;
using Signet.Api.Features.Schemas.UseCases.AddSchema;

namespace Signet.Api.Features.Schemas.Extensions
{
    public static class UseCasesExtensions
    {
        public static WebApplicationBuilder AddSchemaUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUseCaseVoid<AddSchemaInputDto>, AddSchemaUseCase>();

            return builder;
        }
    }
}

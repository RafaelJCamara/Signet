using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Validation.Endpoints.ValidateSchema;
using Signet.Api.Features.Validation.UseCases.ValidateSchema;

namespace Signet.Api.Features.Validation.Extensions
{
    public static class ValidationFeatureExtensions
    {
        public static WebApplicationBuilder AddValidationUseCases(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUseCaseVoid<ValidateSchemaInputDto>, ValidateSchemaUseCase>();

            return builder;
        }
    }
}

using Signet.Api.Common.UseCases;
using Signet.Api.Features.Validation.Endpoints.ValidateSchema;

namespace Signet.Api.Features.Validation.Extensions;

public static class ValidationFeatureExtensions
{
    public static WebApplicationBuilder AddValidationUseCases(this WebApplicationBuilder builder)
    {
        //builder.Services.AddScoped<IUseCase<ValidateSchemaInputDto, bool>, ValidateSchemaUseCase>();

        return builder;
    }
}

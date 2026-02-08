using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Validation.Endpoints.ValidateSchema;

namespace Signet.Api.Features.Validation.UseCases.ValidateSchema
{
    public sealed class ValidateSchemaUseCase : IUseCaseVoid<ValidateSchemaInputDto>
    {
        public ValueTask ExecuteAsync(ValidateSchemaInputDto input, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

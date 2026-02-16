using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Repositories;
using Signet.Api.Features.Validation.Endpoints.ValidateSchema;

namespace Signet.Api.Features.Validation.UseCases.ValidateSchema
{
    public sealed class ValidateSchemaUseCase(ISchemaRepository schemaRepository) : IUseCase<ValidateSchemaInputDto, bool>
    {
        public async ValueTask<bool> ExecuteAsync(ValidateSchemaInputDto input, CancellationToken cancellationToken = default)
        {
            var schema = await schemaRepository.GetByIdAsync(input.SchemaId, cancellationToken);

            return await schema.IsContentValidAgainstSchemaAsync(input.ContentToValidate, cancellationToken);
        }
    }
}

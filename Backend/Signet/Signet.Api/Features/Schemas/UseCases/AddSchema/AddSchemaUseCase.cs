using MongoDB.Driver;
using Signet.Api.Features.Common.UseCases;
using Signet.Api.Features.Schemas.Domain.Entities;
using Signet.Api.Features.Schemas.Endpoints.AddSchema;
using Signet.Api.Infrastructure.Persistence.Mappers;
using Signet.Api.Infrastructure.Persistence.Models;
using System;

namespace Signet.Api.Features.Schemas.UseCases.AddSchema
{
    public sealed class AddSchemaUseCase(IMongoDatabase schemaRegistryDatabase) : IUseCaseVoid<AddSchemaInputDto>
    {
        public async ValueTask ExecuteAsync(AddSchemaInputDto input, CancellationToken cancellationToken = default)
        {
            var newSchema = await Schema.CreateSchemaAsync(
                input.Name,
                input.Description,
                input.Id,
                input.Version,
                input.ChangeLog,
                input.SchemaDefinition
            );

            // persist this
            var collection = schemaRegistryDatabase.GetCollection<MongoSchema>("people");
            await collection.InsertOneAsync(SchemaToMongoSchemaMapper.MapFrom(newSchema));
        }
    }
}

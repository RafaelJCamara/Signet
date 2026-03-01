using MongoDB.Driver;
using Signet.Api.Domain.Entities;
using Signet.Api.Domain.Repositories;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Mappers;
using Signet.Api.Features.Schemas.Infrastructure.Persistence.Models;

namespace Signet.Api.Features.Containers.Common.Infrastructure;

public class SchemaContainerRepository(IMongoDatabase schemaRegistryDatabase) : IRepository<SchemaContainer>
{
    private const string SchemaCollection = "containers";

    private readonly IMongoCollection<MongoSchemaContainer> _collection = schemaRegistryDatabase.GetCollection<MongoSchemaContainer>(SchemaCollection);

    public async Task CreateAsync(SchemaContainer aggregateRoot, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(MongoMappers.MapToMongo(aggregateRoot));
    }

    public async Task<IReadOnlyCollection<SchemaContainer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var cursor = await _collection.FindAsync(Builders<MongoSchemaContainer>.Filter.Empty, cancellationToken: cancellationToken).ConfigureAwait(false);

        var mongoSchemas = await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (mongoSchemas == null || mongoSchemas.Count == 0)
        {
            return Array.Empty<SchemaContainer>();
        }

        var tasks = mongoSchemas.Select(ms => MongoMappers.MapToDomainAsync(ms));

        var schemas = await Task.WhenAll(tasks).ConfigureAwait(false);

        return [.. schemas];
    }

    public async Task<SchemaContainer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var schemaContainer = await _collection.FindAsync(schema => schema.Id == id.ToString());

        return await MongoMappers.MapToDomainAsync(schemaContainer.FirstOrDefault());
    }

    public async Task UpdateAsync(SchemaContainer aggregateRoot, CancellationToken cancellationToken = default)
    {
        var mongoContainer = MongoMappers.MapToMongo(aggregateRoot);

        await _collection.ReplaceOneAsync(
            Builders<MongoSchemaContainer>.Filter.Eq(x => x.Id, mongoContainer.Id),
            mongoContainer,
            cancellationToken: cancellationToken);
    }
}

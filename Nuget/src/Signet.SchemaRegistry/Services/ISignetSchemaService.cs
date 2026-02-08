using Signet.SchemaRegistry.Contracts;

namespace Signet.SchemaRegistry.Services
{
    public interface ISignetSchemaService
    {
        Task<Schema> GetSchemaAsync(string schemaId, string? version);
        Task<Schema> GetOnPublishSchemaAsync(string exchange, string routingKey, string? version);
        Task<Schema> GetOnConsumeSchemaAsync(string queueName, string? version);

        Task<bool> ValidateSchemaAsync(string schemaId, string? version);
        Task<bool> ValidateOnPublishSchemaAsync(string exchange, string routingKey, string? version);
        Task<bool> ValidateOnConsumeSchemaAsync(string queueName, string? version);
    }
}

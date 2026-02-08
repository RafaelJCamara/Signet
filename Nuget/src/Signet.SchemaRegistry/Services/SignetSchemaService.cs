using Signet.SchemaRegistry.Contracts;

namespace Signet.SchemaRegistry.Services
{
    public class SignetSchemaService(IHttpClientFactory httpClientFactory) : ISignetSchemaService
    {
        private readonly HttpClient schemaRegistryClient = httpClientFactory.CreateClient("SignetClient");

        public Task<Schema> GetSchemaAsync(string schemaId, string? version)
        {
            string getSchemaEndpoint = $"/api/schema/{schemaId}";

            

            throw new NotImplementedException();
        }

        public Task<Schema> GetOnPublishSchemaAsync(string exchange, string routingKey, string? version)
        {
            throw new NotImplementedException();
        }

        public Task<Schema> GetOnConsumeSchemaAsync(string queueName, string? version)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateSchemaAsync(string schemaId, string? version)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateOnPublishSchemaAsync(string exchange, string routingKey, string? version)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateOnConsumeSchemaAsync(string queueName, string? version)
        {
            throw new NotImplementedException();
        }
    }
}

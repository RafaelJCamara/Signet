namespace Signet.SchemaRegistry.Utils
{
    public static class SchemaIdGenerator
    {
        public static string GenerateOnPublishSchemaId(string exchange, string routingKey)
        {
            return $"{exchange.ToLowerInvariant()}-{routingKey.ToLowerInvariant()}";
        }
    }
}

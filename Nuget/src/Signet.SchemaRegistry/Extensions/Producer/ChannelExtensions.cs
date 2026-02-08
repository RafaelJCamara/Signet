using RabbitMQ.Client;
using Signet.SchemaRegistry.Exceptions;
using Signet.SchemaRegistry.Services;

namespace Signet.SchemaRegistry.Extensions.Producer
{
    public static class ChannelExtensions
    {
        public static async ValueTask BasicSignetPublishAsync(
            this IChannel channel,
            string exchange,
            string routingKey,
            bool mandatory,
            BasicProperties basicProperties,
            ReadOnlyMemory<byte> body,
            CancellationToken cancellationToken = default,
            bool? validateSchema = false,
            string? schemaVersion = null,
            ISignetSchemaService? signetSchemaService = default)
        {
            if(validateSchema.HasValue && !validateSchema.Value && signetSchemaService is not null)
            {
                // validate schema id and content
                bool isSchemaValid = await signetSchemaService.ValidateOnPublishSchemaAsync(exchange, routingKey, schemaVersion);

                // only move forward if validation was successful
                if (!isSchemaValid)
                {
                    throw new SchemaNotValidException();
                }
            }

            await channel.BasicPublishAsync(
                exchange,
                routingKey,
                mandatory,
                basicProperties,
                body,
                cancellationToken);
        }
    }
}

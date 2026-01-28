using NJsonSchema;
using Signet.Api.Features.Common.Entities;
using Signet.Api.Features.Schemas.Domain.Exceptions;
using System.ComponentModel;

namespace Signet.Api.Features.Schemas.Domain.ValueObjects
{
    public sealed class SchemaDefinition
    {
        public SchemaDefinitionType DefinitionType { get; private set; }
        public string Content { get; private set; }

        private SchemaDefinition() { }

        public static async Task<SchemaDefinition> CreateDefinitionAsync(SchemaDefinitionType definitionType, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Schema Definition should have content.");
            }

            var newDefinition = new SchemaDefinition
            {
                DefinitionType = definitionType,
                Content = content
            };

            await newDefinition.CheckInvariantsAsync().ConfigureAwait(false);

            return newDefinition;
        }

        private async Task CheckInvariantsAsync()
        {
            if(DefinitionType == SchemaDefinitionType.JsonSchema)
            {
                var schema = await JsonSchema.FromJsonAsync(Content).ConfigureAwait(false);

                if(schema is null)
                {
                    throw new JsonSchemaNotValidException();
                }

                return;
            }

            throw new InvalidEnumArgumentException($"Schema definition type {DefinitionType} not supported.");
        }
    }

    public enum SchemaDefinitionType
    {
        JsonSchema = 0
    }
}

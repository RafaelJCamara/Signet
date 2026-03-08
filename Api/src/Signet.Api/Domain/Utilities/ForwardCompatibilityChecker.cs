using NJsonSchema;

namespace Signet.Api.Domain.Utilities;

public class ForwardCompatibilityChecker
{
    public async Task<IReadOnlyList<string>> CheckAsync(string oldSchemaJson, string newSchemaJson)
    {
        var oldSchema = await JsonSchema.FromJsonAsync(oldSchemaJson);
        var newSchema = await JsonSchema.FromJsonAsync(newSchemaJson);

        var issues = new List<string>();

        CompareSchemas(oldSchema, newSchema, "$", issues);

        return issues;
    }

    private void CompareSchemas(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        if (oldSchema == null || newSchema == null)
            return;

        CompareTypes(oldSchema, newSchema, path, issues);
        CompareEnums(oldSchema, newSchema, path, issues);
        CompareObjects(oldSchema, newSchema, path, issues);
        CompareArrays(oldSchema, newSchema, path, issues);
    }

    private void CompareTypes(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        var oldType = oldSchema.Type;
        var newType = newSchema.Type;

        if (newType != JsonObjectType.None &&
            oldType != JsonObjectType.None &&
            !oldType.HasFlag(newType))
        {
            issues.Add($"{path}: Type expanded from {oldType} to {newType}");
        }
    }

    private void CompareEnums(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        if (oldSchema.Enumeration?.Count > 0 && newSchema.Enumeration?.Count > 0)
        {
            var added = newSchema.Enumeration
                .Where(v => !oldSchema.Enumeration.Contains(v))
                .ToList();

            if (added.Any())
            {
                issues.Add($"{path}: Enum values added: {string.Join(",", added)}");
            }
        }
    }

    private void CompareObjects(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        if (!oldSchema.Type.HasFlag(JsonObjectType.Object))
            return;

        var oldProps = oldSchema.ActualProperties;
        var newProps = newSchema.ActualProperties;

        // new properties may break old readers
        foreach (var newProp in newProps)
        {
            if (!oldProps.ContainsKey(newProp.Key))
            {
                if (oldSchema.AllowAdditionalProperties == false)
                {
                    issues.Add($"{path}.{newProp.Key}: New property added but old schema disallows additionalProperties");
                }
            }
            else
            {
                CompareSchemas(
                    oldProps[newProp.Key].ActualSchema,
                    newProp.Value.ActualSchema,
                    $"{path}.{newProp.Key}",
                    issues);
            }
        }
    }

    private void CompareArrays(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        if (!oldSchema.Type.HasFlag(JsonObjectType.Array))
            return;

        var oldItem = oldSchema.Item;
        var newItem = newSchema.Item;

        if (oldItem != null && newItem != null)
        {
            CompareSchemas(oldItem, newItem, $"{path}[]", issues);
        }
    }
}

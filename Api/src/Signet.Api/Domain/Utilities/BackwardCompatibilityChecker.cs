using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NJsonSchema;

public class BackwardCompatibilityChecker
{
    public async Task<IReadOnlyList<string>> CheckAsync(string oldSchemaJson, string newSchemaJson)
    {
        var oldSchema = await JsonSchema.FromJsonAsync(oldSchemaJson);
        var newSchema = await JsonSchema.FromJsonAsync(newSchemaJson);

        var issues = new List<string>();

        CompareSchemas(oldSchema, newSchema, "$", issues);

        return issues;
    }

    private JsonSchema Resolve(JsonSchema schema)
    {
        return schema?.ActualSchema ?? schema;
    }

    private void CompareSchemas(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        var old = Resolve(oldSchema);
        var @new = Resolve(newSchema);

        if (old == null && @new == null)
            return;

        if (old != null && @new == null)
        {
            issues.Add($"{path}: Schema removed");
            return;
        }

        if (old == null && @new != null)
            return;

        CompareTypes(old, @new, path, issues);
        CompareEnums(old, @new, path, issues);

        if (old.Type.HasFlag(JsonObjectType.Object))
            CompareObjects(old, @new, path, issues);

        if (old.Type.HasFlag(JsonObjectType.Array))
            CompareArrays(old, @new, path, issues);
    }

    private void CompareObjects(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        oldSchema = Resolve(oldSchema);
        newSchema = Resolve(newSchema);

        var oldProps = oldSchema.ActualProperties ?? new Dictionary<string, JsonSchemaProperty>();
        var newProps = newSchema.ActualProperties ?? new Dictionary<string, JsonSchemaProperty>();

        var oldRequired = GetRequired(oldSchema);
        var newRequired = GetRequired(newSchema);

        foreach (var required in newRequired)
        {
            if (!oldProps.ContainsKey(required))
            {
                issues.Add($"{path}.{required}: New required property added");
            }
            else if (!oldRequired.Contains(required))
            {
                issues.Add($"{path}.{required}: Existing property became required");
            }
        }

        foreach (var oldProp in oldProps)
        {
            if (!newProps.ContainsKey(oldProp.Key))
            {
                issues.Add($"{path}.{oldProp.Key}: Property removed");
            }
            else
            {
                CompareSchemas(
                    oldProp.Value.ActualSchema,
                    newProps[oldProp.Key].ActualSchema,
                    $"{path}.{oldProp.Key}",
                    issues);
            }
        }
    }

    private HashSet<string> GetRequired(JsonSchema schema)
    {
        var required = new HashSet<string>(
            schema.RequiredProperties ?? Enumerable.Empty<string>());

        foreach (var prop in schema.ActualProperties)
        {
            if (prop.Value.IsRequired)
                required.Add(prop.Key);
        }

        return required;
    }

    private void CompareArrays(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        var oldItem = GetArrayItemSchema(oldSchema);
        var newItem = GetArrayItemSchema(newSchema);

        if (oldItem != null && newItem != null)
        {
            CompareSchemas(oldItem, newItem, $"{path}[]", issues);
        }
        else if (oldItem != null && newItem == null)
        {
            issues.Add($"{path}: Array item schema removed");
        }
    }

    private JsonSchema GetArrayItemSchema(JsonSchema schema)
    {
        return Resolve(schema.Item ?? schema.Items?.FirstOrDefault());
    }

    private void CompareEnums(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        var oldEnum = oldSchema.Enumeration;
        var newEnum = newSchema.Enumeration;

        if (oldEnum != null && oldEnum.Count > 0)
        {
            if (newEnum == null || newEnum.Count == 0)
            {
                issues.Add($"{path}: Enum removed");
                return;
            }

            var removedValues = oldEnum.Where(v => !newEnum.Contains(v)).ToList();

            if (removedValues.Any())
            {
                issues.Add($"{path}: Enum values removed ({string.Join(", ", removedValues)})");
            }
        }
    }

    private void CompareTypes(JsonSchema oldSchema, JsonSchema newSchema, string path, List<string> issues)
    {
        var oldType = oldSchema.Type;
        var newType = newSchema.Type;

        if (oldType == JsonObjectType.None || newType == JsonObjectType.None)
            return;

        // ensure old types are still supported
        if ((oldType & newType) != oldType)
        {
            issues.Add($"{path}: Type changed from {oldType} to {newType}");
        }
    }
}

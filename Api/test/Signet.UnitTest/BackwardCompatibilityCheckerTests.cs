using FluentAssertions;

public class BackwardCompatibilityCheckerTests
{
    private readonly BackwardCompatibilityChecker _checker = new();

    [Fact]
    public async Task Adding_Optional_Property_Should_Be_Backward_Compatible()
    {
        var oldSchema = """
        {
          "type": "object",
          "properties": {
            "name": { "type": "string" }
          }
        }
        """;

        var newSchema = """
        {
          "type": "object",
          "properties": {
            "name": { "type": "string" },
            "age": { "type": "integer" }
          }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().BeEmpty();
    }

    [Fact]
    public async Task Removing_Property_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": "object",
          "properties": {
            "name": { "type": "string" }
          }
        }
        """;

        var newSchema = """
        {
          "type": "object",
          "properties": {
          }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("Property removed"));
    }

    [Fact]
    public async Task Adding_New_Required_Property_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": "object",
          "properties": {
            "name": { "type": "string" }
          }
        }
        """;

        var newSchema = """
        {
          "type": "object",
          "required": ["age"],
          "properties": {
            "name": { "type": "string" },
            "age": { "type": "integer" }
          }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("New required property added"));
    }

    [Fact]
    public async Task Property_Becoming_Required_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": "object",
          "properties": {
            "name": { "type": "string" }
          }
        }
        """;

        var newSchema = """
        {
          "type": "object",
          "required": ["name"],
          "properties": {
            "name": { "type": "string" }
          }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("became required"));
    }

    [Fact]
    public async Task Enum_Value_Removal_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": "string",
          "enum": ["A", "B", "C"]
        }
        """;

        var newSchema = """
        {
          "type": "string",
          "enum": ["A", "B"]
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("Enum values removed"));
    }

    [Fact]
    public async Task Enum_Value_Addition_Should_Be_Compatible()
    {
        var oldSchema = """
        {
          "type": "string",
          "enum": ["A", "B"]
        }
        """;

        var newSchema = """
        {
          "type": "string",
          "enum": ["A", "B", "C"]
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().BeEmpty();
    }

    [Fact]
    public async Task Type_Restriction_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": ["string", "null"]
        }
        """;

        var newSchema = """
        {
          "type": "string"
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("Type changed"));
    }

    [Fact]
    public async Task Nested_Object_Property_Removal_Should_Be_Detected()
    {
        var oldSchema = """
        {
          "type": "object",
          "properties": {
            "address": {
              "type": "object",
              "properties": {
                "street": { "type": "string" }
              }
            }
          }
        }
        """;

        var newSchema = """
        {
          "type": "object",
          "properties": {
            "address": {
              "type": "object",
              "properties": {
              }
            }
          }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("street"));
    }

    [Fact]
    public async Task Array_Item_Type_Change_Should_Be_Breaking()
    {
        var oldSchema = """
        {
          "type": "array",
          "items": { "type": "string" }
        }
        """;

        var newSchema = """
        {
          "type": "array",
          "items": { "type": "integer" }
        }
        """;

        var issues = await _checker.CheckAsync(oldSchema, newSchema);

        issues.Should().Contain(x => x.Contains("Type changed"));
    }

    [Fact]
    public async Task Currency_Becoming_Required_Should_Be_Breaking()
    {
        var oldSchema = """
    { 
      "$schema": "http://json-schema.org/draft-07/schema#",
      "type": "object",
      "properties": {
        "orderId": { "type": "string" },
        "amount": { "type": "number", "minimum": 0 },
        "currency": { "type": "string", "minLength": 3, "maxLength": 3 }
      },
      "required": ["orderId", "amount"]
    }
    """;

        var newSchema = """
    { 
      "$schema": "http://json-schema.org/draft-07/schema#",
      "type": "object",
      "properties": {
        "orderId": { "type": "string" },
        "amount": { "type": "number", "minimum": 0 },
        "currency": { "type": "string", "minLength": 3, "maxLength": 3 }
      },
      "required": ["orderId", "amount", "currency"]
    }
    """;

        var checker = new BackwardCompatibilityChecker();

        var issues = await checker.CheckAsync(oldSchema, newSchema);

        issues.Should().ContainSingle(i =>
            i.Contains("$.currency") &&
            i.Contains("required"));
    }
}

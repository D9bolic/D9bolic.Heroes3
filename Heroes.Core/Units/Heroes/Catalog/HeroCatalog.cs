using System.Text.Json;
using System.Text.Json.Nodes;
using Json.Schema;

namespace Heroes.Units.Heroes.Catalog;

public sealed class HeroCatalog : IHeroCatalog
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
    };

    // Json.Schema's global SchemaRegistry rejects re-registering the same $id;
    // cache the parsed schema per file so repeated loads (tests, hot reload)
    // don't trip "Overwriting registered schemas is not permitted."
    private static readonly Dictionary<string, JsonSchema> SchemaCache = new();
    private static readonly object SchemaCacheLock = new();

    private readonly Dictionary<string, HeroDefinition> _definitions;

    private HeroCatalog(Dictionary<string, HeroDefinition> definitions)
    {
        _definitions = definitions;
    }

    public IReadOnlyCollection<string> Ids => _definitions.Keys;

    public HeroDefinition GetDefinition(string id)
    {
        if (!_definitions.TryGetValue(id, out var definition))
        {
            throw new KeyNotFoundException(
                $"No hero definition for id '{id}'. Known ids: {string.Join(", ", _definitions.Keys)}.");
        }

        return definition;
    }

    public IHero Create(string id)
    {
        return new CatalogHero(GetDefinition(id));
    }

    public static HeroCatalog LoadFromDirectory(string assetsDirectory, string schemaPath)
    {
        if (!Directory.Exists(assetsDirectory))
        {
            throw new DirectoryNotFoundException(
                $"Hero catalog directory '{assetsDirectory}' does not exist.");
        }

        if (!File.Exists(schemaPath))
        {
            throw new FileNotFoundException(
                $"Hero schema file '{schemaPath}' does not exist.", schemaPath);
        }

        var schema = LoadSchemaCached(schemaPath);
        var definitions = new Dictionary<string, HeroDefinition>(StringComparer.OrdinalIgnoreCase);

        foreach (var path in Directory.EnumerateFiles(assetsDirectory, "*.json").OrderBy(p => p))
        {
            var fileName = Path.GetFileName(path);
            if (fileName.StartsWith('_'))
            {
                continue;
            }

            var json = File.ReadAllText(path);
            var node = JsonNode.Parse(json) as JsonObject
                ?? throw new InvalidOperationException(
                    $"Hero file '{fileName}' must be a JSON object.");

            // Editor-only metadata pointing to the schema in source — strip
            // before validating so additionalProperties: false isn't tripped.
            node.Remove("$schema");

            using var document = JsonDocument.Parse(node.ToJsonString());
            var validation = schema.Evaluate(document.RootElement, new EvaluationOptions
            {
                OutputFormat = OutputFormat.List,
            });

            if (!validation.IsValid)
            {
                var errors = string.Join("; ", FlattenErrors(validation));
                throw new InvalidOperationException(
                    $"Hero file '{fileName}' failed schema validation: {errors}");
            }

            var definition = node.Deserialize<HeroDefinition>(JsonOptions)
                ?? throw new InvalidOperationException($"Failed to deserialize hero '{fileName}'.");

            if (definitions.ContainsKey(definition.Id))
            {
                throw new InvalidOperationException(
                    $"Duplicate hero id '{definition.Id}' (file '{fileName}').");
            }

            definitions.Add(definition.Id, definition);
        }

        return new HeroCatalog(definitions);
    }

    private static JsonSchema LoadSchemaCached(string schemaPath)
    {
        var key = Path.GetFullPath(schemaPath);
        lock (SchemaCacheLock)
        {
            if (!SchemaCache.TryGetValue(key, out var schema))
            {
                schema = JsonSchema.FromFile(key);
                SchemaCache[key] = schema;
            }
            return schema;
        }
    }

    private static IEnumerable<string> FlattenErrors(EvaluationResults results)
    {
        if (results.Errors is { } errors && errors.Count > 0)
        {
            foreach (var error in errors)
            {
                yield return $"{results.InstanceLocation} {error.Key}: {error.Value}";
            }
        }

        if (results.Details is { } details)
        {
            foreach (var detail in details)
            {
                foreach (var msg in FlattenErrors(detail))
                {
                    yield return msg;
                }
            }
        }
    }
}

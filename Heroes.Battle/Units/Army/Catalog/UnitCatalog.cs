using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Heroes.Events;
using Json.Schema;

namespace Heroes.Battle.Units.Army.Catalog;

public sealed class UnitCatalog : IUnitCatalog
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    };

    private readonly Dictionary<string, UnitDefinition> _definitions;

    private UnitCatalog(Dictionary<string, UnitDefinition> definitions)
    {
        _definitions = definitions;
    }

    public IReadOnlyCollection<string> Ids => _definitions.Keys;

    public UnitDefinition GetDefinition(string id)
    {
        if (!_definitions.TryGetValue(id, out var definition))
        {
            throw new KeyNotFoundException(
                $"No unit definition for id '{id}'. Known ids: {string.Join(", ", _definitions.Keys)}.");
        }

        return definition;
    }

    public IUnit Create(string id, Point coordinates, IGameEventBus eventBus)
    {
        return new CatalogUnit(GetDefinition(id), coordinates, eventBus);
    }

    public static UnitCatalog LoadFromDirectory(string assetsDirectory, string schemaPath)
    {
        if (!Directory.Exists(assetsDirectory))
        {
            throw new DirectoryNotFoundException(
                $"Unit catalog directory '{assetsDirectory}' does not exist.");
        }

        if (!File.Exists(schemaPath))
        {
            throw new FileNotFoundException(
                $"Unit schema file '{schemaPath}' does not exist.", schemaPath);
        }

        var schema = JsonSchema.FromFile(schemaPath);
        var definitions = new Dictionary<string, UnitDefinition>(StringComparer.OrdinalIgnoreCase);

        foreach (var path in Directory.EnumerateFiles(assetsDirectory, "*.json").OrderBy(p => p))
        {
            var fileName = Path.GetFileName(path);
            if (fileName.StartsWith('_'))
            {
                continue;
            }

            var json = File.ReadAllText(path);
            using var document = JsonDocument.Parse(json);

            var validation = schema.Evaluate(document.RootElement, new EvaluationOptions
            {
                OutputFormat = OutputFormat.List,
            });

            if (!validation.IsValid)
            {
                var errors = string.Join("; ", FlattenErrors(validation));
                throw new InvalidOperationException(
                    $"Unit file '{fileName}' failed schema validation: {errors}");
            }

            var definition = document.RootElement.Deserialize<UnitDefinition>(JsonOptions)
                ?? throw new InvalidOperationException($"Failed to deserialize unit '{fileName}'.");

            if (definitions.ContainsKey(definition.Name))
            {
                throw new InvalidOperationException(
                    $"Duplicate unit id '{definition.Name}' (file '{fileName}').");
            }

            definitions.Add(definition.Name, definition);
        }

        return new UnitCatalog(definitions);
    }

    private static IEnumerable<string> FlattenErrors(EvaluationResults results)
    {
        if (results.Errors is { Count: > 0 })
        {
            foreach (var error in results.Errors)
            {
                yield return $"{results.InstanceLocation} {error.Key}: {error.Value}";
            }
        }

        foreach (var detail in results.Details)
        {
            foreach (var msg in FlattenErrors(detail))
            {
                yield return msg;
            }
        }
    }
}

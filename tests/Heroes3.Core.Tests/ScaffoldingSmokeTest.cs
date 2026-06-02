using System.Linq;

namespace Heroes3.Core.Tests;

public class ScaffoldingSmokeTest
{
    [Fact]
    public void CoreAssembly_IsReferenced()
    {
        var coreAssembly = typeof(AssemblyMarker).Assembly;
        Assert.Equal("Heroes3.Core", coreAssembly.GetName().Name);
    }

    [Fact]
    public void CoreAssembly_HasNoUiDependencies()
    {
        var coreAssembly = typeof(AssemblyMarker).Assembly;
        var forbiddenPrefixes = new[]
        {
            "Avalonia",
            "PresentationFramework",
            "PresentationCore",
            "WindowsBase",
            "System.Windows.Forms",
        };

        var offenders = coreAssembly
            .GetReferencedAssemblies()
            .Select(a => a.Name ?? string.Empty)
            .Where(name => forbiddenPrefixes.Any(prefix =>
                name.Equals(prefix, System.StringComparison.Ordinal) ||
                name.StartsWith(prefix + ".", System.StringComparison.Ordinal)))
            .ToArray();

        Assert.Empty(offenders);
    }
}

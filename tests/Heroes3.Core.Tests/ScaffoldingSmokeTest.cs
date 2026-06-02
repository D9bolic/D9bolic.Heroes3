namespace Heroes3.Core.Tests;

public class ScaffoldingSmokeTest
{
    [Fact]
    public void CoreAssembly_IsReferenced()
    {
        var coreAssembly = typeof(Heroes3.Core.AssemblyMarker).Assembly;
        Assert.Equal("Heroes3.Core", coreAssembly.GetName().Name);
    }
}

namespace D9bolic.Assets;

/// <summary>Pluggable store for unit/hero/UI images served by the host.</summary>
public interface IImageStore
{
    bool Exists(string key);

    Stream Open(string key);
}

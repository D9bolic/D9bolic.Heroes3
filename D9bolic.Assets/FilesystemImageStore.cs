using Microsoft.Extensions.Options;

namespace D9bolic.Assets;

public sealed class FilesystemImageStore : IImageStore
{
    private readonly string _rootFullPath;

    public FilesystemImageStore(IOptions<FilesystemImageStoreOptions> options)
        : this(options.Value.RootPath)
    {
    }

    public FilesystemImageStore(string rootPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);
        _rootFullPath = Path.GetFullPath(rootPath);
    }

    /// <summary>Absolute path to the configured root directory.</summary>
    public string RootPath => _rootFullPath;

    public bool Exists(string key)
        => TryResolve(key, out var path) && File.Exists(path);

    public Stream Open(string key)
    {
        if (!TryResolve(key, out var path) || !File.Exists(path))
        {
            throw new FileNotFoundException($"Image asset '{key}' was not found.", key);
        }

        return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>Resolves <paramref name="key"/> to an absolute path inside the root, refusing traversal.</summary>
    public bool TryResolve(string key, out string fullPath)
    {
        fullPath = string.Empty;

        if (string.IsNullOrWhiteSpace(key))
        {
            return false;
        }

        // Reject absolute paths and rooted segments outright; only relative keys are supported.
        if (Path.IsPathRooted(key) || key.Contains(':'))
        {
            return false;
        }

        var normalized = key.Replace('\\', '/').TrimStart('/');
        var combined = Path.GetFullPath(Path.Combine(_rootFullPath, normalized));

        // Guard against ".." escapes — final path must stay under the root.
        var rootWithSep = _rootFullPath.EndsWith(Path.DirectorySeparatorChar)
            ? _rootFullPath
            : _rootFullPath + Path.DirectorySeparatorChar;

        if (!combined.StartsWith(rootWithSep, StringComparison.OrdinalIgnoreCase)
            && !string.Equals(combined, _rootFullPath, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        fullPath = combined;
        return true;
    }
}

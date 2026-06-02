namespace D9bolic.Assets;

public static class ImageContentType
{
    private static readonly Dictionary<string, string> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        [".png"] = "image/png",
        [".jpg"] = "image/jpeg",
        [".jpeg"] = "image/jpeg",
        [".gif"] = "image/gif",
        [".webp"] = "image/webp",
        [".svg"] = "image/svg+xml",
        [".bmp"] = "image/bmp",
        [".ico"] = "image/x-icon",
        [".avif"] = "image/avif",
    };

    public static string FromKey(string key)
    {
        var ext = Path.GetExtension(key);
        return ext is not null && Map.TryGetValue(ext, out var mime)
            ? mime
            : "application/octet-stream";
    }
}

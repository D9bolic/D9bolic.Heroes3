using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace D9bolic.Assets;

public static class ImageStoreEndpointExtensions
{
    private static readonly TimeSpan DefaultMaxAge = TimeSpan.FromDays(7);

    /// <summary>Maps GET <c>/images/{**key}</c> backed by the registered <see cref="IImageStore"/>.</summary>
    public static IEndpointConventionBuilder MapImageStore(
        this IEndpointRouteBuilder endpoints,
        string pattern = "/images/{**key}")
    {
        return endpoints.MapGet(pattern, (string key, IImageStore store, HttpContext http) =>
        {
            if (string.IsNullOrWhiteSpace(key) || !store.Exists(key))
            {
                return Results.NotFound();
            }

            // For filesystem-backed stores we can serve a strong ETag + Last-Modified and
            // honour conditional GETs. Other implementations get the same headers from the
            // underlying stream length once it's opened.
            DateTimeOffset? lastModified = null;
            string? etag = null;

            if (store is FilesystemImageStore fs && fs.TryResolve(key, out var path))
            {
                var info = new FileInfo(path);
                if (!info.Exists)
                {
                    return Results.NotFound();
                }

                lastModified = info.LastWriteTimeUtc;
                etag = BuildETag(info);
            }

            // Conditional GET: short-circuit before opening the stream.
            var ifNoneMatch = http.Request.Headers.IfNoneMatch.ToString();
            if (etag is not null && !string.IsNullOrEmpty(ifNoneMatch) && ifNoneMatch == etag)
            {
                ApplyCacheHeaders(http, etag, lastModified);
                return Results.StatusCode(StatusCodes.Status304NotModified);
            }

            if (lastModified is { } lm
                && DateTimeOffset.TryParse(http.Request.Headers.IfModifiedSince, out var ims)
                && lm <= ims)
            {
                ApplyCacheHeaders(http, etag, lastModified);
                return Results.StatusCode(StatusCodes.Status304NotModified);
            }

            Stream stream;
            try
            {
                stream = store.Open(key);
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound();
            }

            ApplyCacheHeaders(http, etag, lastModified);

            return Results.File(
                stream,
                contentType: ImageContentType.FromKey(key),
                lastModified: lastModified,
                entityTag: etag is null ? null : new(etag));
        });
    }

    private static void ApplyCacheHeaders(HttpContext http, string? etag, DateTimeOffset? lastModified)
    {
        var headers = http.Response.Headers;
        headers.CacheControl = $"public,max-age={(int)DefaultMaxAge.TotalSeconds}";
        if (etag is not null)
        {
            headers.ETag = etag;
        }
        if (lastModified is { } lm)
        {
            headers.LastModified = lm.ToString("R");
        }
    }

    private static string BuildETag(FileInfo info)
        => $"\"{info.LastWriteTimeUtc.Ticks:x}-{info.Length:x}\"";
}

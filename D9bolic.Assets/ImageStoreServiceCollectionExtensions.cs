using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace D9bolic.Assets;

public static class ImageStoreServiceCollectionExtensions
{
    /// <summary>Registers <see cref="FilesystemImageStore"/> as <see cref="IImageStore"/>.</summary>
    public static IServiceCollection AddFilesystemImageStore(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = FilesystemImageStoreOptions.SectionName)
    {
        services.AddOptions<FilesystemImageStoreOptions>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateOnStart();

        services.AddSingleton<IImageStore>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<FilesystemImageStoreOptions>>().Value;
            return new FilesystemImageStore(options.RootPath);
        });

        return services;
    }

    /// <summary>Registers <see cref="FilesystemImageStore"/> with an explicit root path.</summary>
    public static IServiceCollection AddFilesystemImageStore(
        this IServiceCollection services,
        string rootPath)
    {
        services.AddSingleton<IImageStore>(_ => new FilesystemImageStore(rootPath));
        return services;
    }
}

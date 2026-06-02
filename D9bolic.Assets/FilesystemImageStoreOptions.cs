namespace D9bolic.Assets;

public sealed class FilesystemImageStoreOptions
{
    public const string SectionName = "ImageStore";

    /// <summary>Absolute or content-root-relative directory containing the image tree.</summary>
    public string RootPath { get; set; } = "wwwroot/assets/images";
}

namespace SharedConfig;

public record FileAssetInfo
{
    public string Name { get; set; } = string.Empty;
    public string FeatureDirectory { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
}

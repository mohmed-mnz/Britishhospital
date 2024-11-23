namespace SharedConfig;

public class JWT
{

    public string key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirytimeinMinutes { get; set; }

}

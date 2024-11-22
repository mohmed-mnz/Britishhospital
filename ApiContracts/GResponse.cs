using System.Text.Json.Serialization;

namespace ApiContracts;

public class GResponse<T>
{
    [JsonPropertyName("Data")]
    public T? Data { get; set; }

    [JsonPropertyName("IsSucceeded")]
    public bool IsSucceeded { get; set; }

    [JsonPropertyName("Error")]
    public string? Error { get; set; }

    [JsonPropertyName("ErrorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("StatusCode")]
    public string? StatusCode { get; set; }

    public static GResponse<T> CreateSuccess(T? Data)
    {
        return new GResponse<T>
        {
            IsSucceeded = true,
            Data = Data
        };
    }

    public static GResponse<T> CreateFailure(string? StatusCode, string? ErrorMessage)
    {
        return new GResponse<T>
        {
            IsSucceeded = false,
            ErrorMessage = ErrorMessage,
            StatusCode = StatusCode
        };
    }
}

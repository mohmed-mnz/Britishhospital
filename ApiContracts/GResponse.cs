using System.Text.Json.Serialization;

namespace ApiContracts;

public class GResponse<T>
{
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("isSucceeded")]
    public bool IsSucceeded { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("statusCode")]
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

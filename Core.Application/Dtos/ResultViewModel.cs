using System.Text.Json.Serialization;

namespace Core.Application.Dtos;

public class ResultViewModel
{

    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
    [JsonPropertyName("returnObject")]
    public object ReturnObject { get; set; }
    public ResultViewModel BindResultViewModel(bool success, string message, int statusCode, object returnObject)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
        ReturnObject = returnObject;

        return this;
    }
}

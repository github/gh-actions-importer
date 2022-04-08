using Newtonsoft.Json;

namespace Valet.Models;

public class UserAuthenticationResponse
{
    [JsonConstructor]
    public UserAuthenticationResponse(string? accessToken, string? message, string? error)
    {
        AccessToken = accessToken;
        Message = message;
        Error = error;
    }
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
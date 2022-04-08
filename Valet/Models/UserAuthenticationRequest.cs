using Newtonsoft.Json;

namespace Valet.Models;

public class UserAuthenticationRequest
{
    [JsonConstructor]
    public UserAuthenticationRequest(string clientId, string deviceCode)
    {
        ClientId = clientId;
        DeviceCode = deviceCode;
    }

    [JsonProperty("client_id")]
    public string ClientId { get; set; }

    [JsonProperty("device_code")]
    public string DeviceCode { get; set; }

    [JsonProperty("grant_type")]
    public string GrantType { get; } = "urn:ietf:params:oauth:grant-type:device_code";
}
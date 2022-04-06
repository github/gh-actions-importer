using Newtonsoft.Json;

namespace Valet.Models;

public class DeviceCodeRequest
{
    [JsonConstructor]
    public DeviceCodeRequest(string clientId, string scope)
    {
        ClientId = clientId;
        Scope = scope;
    }

    [JsonProperty("client_id")]
    public string ClientId { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }
}
using Newtonsoft.Json;

namespace Valet.Models;

public class DeviceCodeResponse
{
    [JsonConstructor]
    public DeviceCodeResponse(string verificationUri, string deviceCode, string userCode, int? interval, int expiresIn)
    {
        VerificationUri = verificationUri;
        DeviceCode = deviceCode;
        UserCode = userCode;
        Interval = interval;
        ExpiresIn = expiresIn;
    }

    [JsonProperty("verification_uri")]
    public string VerificationUri { get; set; }

    [JsonProperty("device_code")]
    public string DeviceCode { get; set; }

    [JsonProperty("user_code")]
    public string UserCode { get; set; }

    [JsonProperty("interval")]
    public int? Interval { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
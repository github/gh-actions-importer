using Valet.Interfaces;
using Valet.Models;

namespace Valet.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private const string GitHubInstanceUrl = "https://github.com";
    private const string ClientId = "44e647121433c49e8c11";
    
    public AuthenticationService(HttpMessageHandler? handler = null) 
        : base(GitHubInstanceUrl, handler)
    {
    }
    
    public async Task<string> GetAccessTokenAsync()
    {
        var deviceCode = await GetDeviceCodeAsync().ConfigureAwait(false);
        
        Console.WriteLine($"Please authenticate at '{deviceCode.VerificationUri}' using code: {deviceCode.UserCode}");

        var startTime = DateTime.Now;

        while (DateTime.Now - startTime < TimeSpan.FromSeconds(deviceCode.ExpiresIn))
        {
            Thread.Sleep(deviceCode.Interval ?? 5);

            var userAuthenticated = await CheckUserAuthenticatedAsync(deviceCode.DeviceCode).ConfigureAwait(false);

            if (userAuthenticated.Error == "access_denied")
            {
                throw new Exception("Authentication failed: access denied");
            }
            
            if (!string.IsNullOrWhiteSpace(userAuthenticated.AccessToken))
            {
                Console.WriteLine("Authenticated");

                return userAuthenticated.AccessToken;
            }
        }

        throw new Exception("No authentication attempt received.");
    }

    private Task<UserAuthenticationResponse> CheckUserAuthenticatedAsync(string deviceCode)
    {
        return PostAsync<UserAuthenticationRequest, UserAuthenticationResponse>(
            "login/oauth/access_token",
            new UserAuthenticationRequest(ClientId, deviceCode),
            null
        );
    }

    private Task<DeviceCodeResponse> GetDeviceCodeAsync()
    {
        return PostAsync<DeviceCodeRequest, DeviceCodeResponse>(
            "login/device/code",
            new DeviceCodeRequest(ClientId, "read:packages"),
            null
        );
    }
}
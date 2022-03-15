using Valet.Interfaces;

namespace Valet.Services;

public class AuthenticationService : IAuthenticationService
{
    public Task<(string username, string token)> GetAccessTokenAsync()
    {
        throw new NotImplementedException();
    }
}
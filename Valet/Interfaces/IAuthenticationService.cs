namespace Valet.Interfaces;

public interface IAuthenticationService
{
    Task<(string username, string token)> GetAccessTokenAsync();
}
namespace Valet.Interfaces;

public interface IAuthenticationService
{
    Task<string> GetAccessTokenAsync();
}
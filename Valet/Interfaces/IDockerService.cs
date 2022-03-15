namespace Valet.Interfaces;

public interface IDockerService
{
    Task<bool> UpdateImageAsync(string image, string server, string username, string password);
}
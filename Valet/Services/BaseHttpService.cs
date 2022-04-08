using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Valet.Services;

public abstract class BaseHttpService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializer _serializer;
    private readonly string _baseAddress;

    protected BaseHttpService(string baseAddress, HttpMessageHandler? handler)
    {
        _httpClient = handler != null ? new HttpClient(handler) : new HttpClient();
        _baseAddress = baseAddress;
        _serializer = new JsonSerializer();
    }

    protected Task<T> GetAsync<T>(
        string requestUri,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        return SendAndDeserializeAsync<T>(HttpMethod.Get, requestUri, null, authHeader, cancellationToken);
    }

    protected async Task<bool> PostAsync<T>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        var response = await SendAsync(HttpMethod.Post, requestUri, jsonRequest, authHeader, cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    protected Task<TR> PostAsync<T, TR>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        return SendAndDeserializeAsync<TR>(HttpMethod.Post, requestUri, jsonRequest, authHeader, cancellationToken);
    }

    protected async Task<bool> PutAsync<T>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        var response = await SendAsync(HttpMethod.Put, requestUri, jsonRequest, authHeader, cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    protected Task<TR> PutAsync<T, TR>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        return SendAndDeserializeAsync<TR>(HttpMethod.Put, requestUri, jsonRequest, authHeader, cancellationToken);
    }

    protected async Task<bool> PatchAsync<T>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        var response = await SendAsync(new HttpMethod("Patch"), requestUri, jsonRequest, authHeader, cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    protected Task<TR> PatchAsync<T, TR>(
        string requestUri,
        T content,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var jsonRequest = content != null ? JsonConvert.SerializeObject(content) : null;
        return SendAndDeserializeAsync<TR>(new HttpMethod("Patch"), requestUri, jsonRequest, authHeader, cancellationToken);
    }

    protected async Task<bool> DeleteAsync(
        string requestUri,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(HttpMethod.Delete, requestUri, null, authHeader, cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    protected async Task<HttpResponseMessage> SendAsync(
        HttpMethod requestType,
        string requestUri,
        string? json,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage(requestType, new Uri($"{_baseAddress}/{requestUri}"));

        if (json != null)
        {
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = authHeader;

        var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return response;
    }

    protected async Task<T?> DeserializeAsync<T>(
        HttpResponseMessage response)
    {
        using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
        using (var reader = new StreamReader(stream))
        using (var jsonReader = new JsonTextReader(reader))
            return _serializer.Deserialize<T>(jsonReader);
    }

    protected async Task<T> SendAndDeserializeAsync<T>(
        HttpMethod requestType,
        string requestUri,
        string? jsonRequest,
        AuthenticationHeaderValue? authHeader,
        CancellationToken cancellationToken = default)
    {
        var response = await SendAsync(requestType, requestUri, jsonRequest, authHeader, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await DeserializeAsync<T>(response);
    }
}
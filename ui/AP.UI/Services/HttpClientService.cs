using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AP.UI.Services;

public interface IHttpClientService
{
    Task<T> GetAsync<T>(string endpoint);
    Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
}

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly IApiConnectionService _apiConnectionService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpClientService(HttpClient httpClient, IApiConnectionService apiConnectionService,
    IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _apiConnectionService = apiConnectionService;
        _httpContextAccessor = httpContextAccessor;
        _httpClient.BaseAddress = new Uri(_apiConnectionService.GetApiBaseUrl());
    }
    private void AddAuthorizationHeader()
    {
        var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
        _httpClient.DefaultRequestHeaders.Authorization = null; // Önce temizle
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
    public async Task<T> GetAsync<T>(string endpoint)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        AddAuthorizationHeader();
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
     
        var response = await _httpClient.PostAsync(endpoint, content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API call failed with status code {response.StatusCode}");
        }
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(responseContent);
    }
}

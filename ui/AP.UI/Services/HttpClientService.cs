using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AP.UI.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    }

    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IApiConnectionService _apiConnectionService;

        public HttpClientService(HttpClient httpClient, IApiConnectionService apiConnectionService)
        {
            _httpClient = httpClient;
            _apiConnectionService = apiConnectionService;
            _httpClient.BaseAddress = new Uri(_apiConnectionService.GetApiBaseUrl());
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API call failed with status code {response.StatusCode}");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }
    }
}

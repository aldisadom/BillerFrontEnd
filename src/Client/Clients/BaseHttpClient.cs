using Contracts.Responses;
using Domain.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace Clients.Clients;

public class BaseHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _urlBase;

    public BaseHttpClient(IHttpClientFactory httpClientFactory, string urlBase)
    {
        _urlBase = urlBase;
        _httpClientFactory = httpClientFactory;
    }

    public Uri GenerateUrl(string endpoint, Dictionary<string, string>? queryParameters = null)
    {
        StringBuilder urlBuilder = new($"{_urlBase}/{endpoint}");

        if (queryParameters?.Count > 0)
        {
            urlBuilder.Append('?');
            urlBuilder.Append(string.Join("&", queryParameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}")));
        }

        return new Uri(urlBuilder.ToString());
    }

    public static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headerParameters = null)
    {
        if (headerParameters?.Count > 0)
        {
            foreach (var (key, value) in headerParameters)
            {
                request.Headers.Add(key, value);
            }
        }
    }
    private void DecodeResponseError(string body, HttpResponseMessage response)
    {
        string errorMessage = $"Failed to get data from client {_urlBase}, error code: {response.StatusCode}";

        try
        {
            ErrorResponse? error = JsonConvert.DeserializeObject<ErrorResponse>(body);

            if (error is null)
                errorMessage += $", with body: {body}";
            else
                errorMessage += $", with message: {error}";

        }
        catch (Exception)
        {
            errorMessage += $", with body: {body}";
        }

        throw new ClientAPIException(errorMessage);
    }

    private T DecodeResponse<T>(string body)
    {
        return JsonConvert.DeserializeObject<T>(body)
            ?? throw new ClientAPIException($"Failed to deserialize data from client {_urlBase}, with body: {body}");
    }
    public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        //configure
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Get, url);
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            DecodeResponseError(responseBody, response);

        return DecodeResponse<T>(responseBody);
    }

    public async Task<T2> PostAsync<T1, T2>(string endpoint, T1 data, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        //configure
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
        };
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            DecodeResponseError(responseBody, response);

        return DecodeResponse<T2>(responseBody);
    }

    public async Task PutAsync<T>(string endpoint, T data, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        //configure
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Put, url)
        {
            Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
        };
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            DecodeResponseError(responseBody, response);
    }

    public async Task DeleteAsync(string endpoint, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        //configure
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Delete, url);
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            DecodeResponseError(responseBody, response);
    }
}

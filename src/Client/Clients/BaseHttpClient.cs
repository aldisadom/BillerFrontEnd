using Common;
using Contracts.Responses;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace Clients.Clients;

public class BaseHttpClient
{
    private readonly string _urlBase;

    public BaseHttpClient(string urlBase)
    {
        _urlBase = urlBase;
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
    private ErrorModel DecodeResponseError(string body, HttpResponseMessage response)
    {
        ErrorModel error = new()
        {
            StatusCode = response.StatusCode
        };

        try
        {
            ErrorResponse? errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(body);

            if (errorResponse is null)
                error.Message = body;
            else
            {
                error.Message = errorResponse.Message;
                error.ExtendedMessage = errorResponse.ExtendedMessage;
            }
        }
        catch (Exception)
        {
            error.Message = body;
        }

        Log.Error("{Message}", JsonConvert.SerializeObject(error.ToString()));
        return error;
    }

    private Result<T> DecodeResponse<T>(string body, HttpResponseMessage response)
    {
        var deserialised = JsonConvert.DeserializeObject<T>(body);

        if (deserialised is null)
            return new ErrorModel("Failed to deserialise", $"With body:{body}", response.StatusCode);

        return deserialised;
    }

    public async Task<Result<T>> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = new();

        //configure 
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Get, url);
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return DecodeResponseError(responseBody, response);

        return DecodeResponse<T>(responseBody, response);
    }

    public async Task<Result<T2>> PostAsync<T1, T2>(string endpoint, T1 data, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = new();

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
            return DecodeResponseError(responseBody, response);

        return DecodeResponse<T2>(responseBody, response);
    }

    public async Task<Result<bool>> PutAsync<T>(string endpoint, T data, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = new();

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
            return DecodeResponseError(responseBody, response);

        return true;
    }

    public async Task<Result<bool>> DeleteAsync(string endpoint, Dictionary<string, string>? queryParameters = null, Dictionary<string, string>? headerParameters = null)
    {
        HttpClient client = new();

        //configure
        Uri url = GenerateUrl(endpoint, queryParameters);
        HttpRequestMessage request = new(HttpMethod.Delete, url);
        AddHeaders(request, headerParameters);

        //send
        HttpResponseMessage response = await client.SendAsync(request);

        //decode
        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return DecodeResponseError(responseBody, response);

        return true;
    }
}

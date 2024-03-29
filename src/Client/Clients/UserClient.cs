﻿using Clients.Interfaces;
using Contracts.Requests.User;
using Contracts.Responses;
using Contracts.Responses.User;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class UserClient : IUserClient
{
    private readonly BaseHttpClient _userHttpClient;

    public UserClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string userUrl = clientOptions.Value.UserUrl
            ?? throw new ArgumentNullException($"User client URL is missing");

        _userHttpClient = new(httpClientFactory, userUrl);
    }

    public async Task<UserLoginResponse> Login(UserLoginRequest user)
    {
        Dictionary<string, string> headers = new()
        {
            { "Email", user.Email },
            { "Password", user.Password }
        };

        return await _userHttpClient.GetAsync<UserLoginResponse>("User/Login", headers);
    }

    public async Task<UserListResponse> Get()
    {
        return await _userHttpClient.GetAsync<UserListResponse>("User");
    }

    public async Task<UserResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<UserResponse>($"User/{id}");
    }

    public async Task<AddResponse> Add(UserAddRequest user)
    {
        return await _userHttpClient.PostAsync<UserAddRequest, AddResponse>($"User", user);
    }

    public async Task Update(UserUpdateRequest user)
    {
        await _userHttpClient.PutAsync($"User", user);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"User/{id}");
    }
}

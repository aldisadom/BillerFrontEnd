using Clients.Interfaces;
using Common;
using Contracts.Requests.User;
using Contracts.Responses;
using Contracts.Responses.User;

namespace Clients.Clients;

public class UserClient : IUserClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "users";

    public UserClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<Result<UserLoginResponse>> Login(UserLoginRequest user)
    {
        return await _userHttpClient.PostAsync<UserLoginRequest, UserLoginResponse>($"{_controller}/Login", user);
    }

    public async Task<Result<UserListResponse>> Get()
    {
        return await _userHttpClient.GetAsync<UserListResponse>($"{_controller}");
    }

    public async Task<Result<UserResponse?>> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<UserResponse?>($"{_controller}/{id}");
    }

    public async Task<Result<AddResponse>> Add(UserAddRequest user)
    {
        return await _userHttpClient.PostAsync<UserAddRequest, AddResponse>($"{_controller}", user);
    }

    public async Task<Result<bool>> Update(UserUpdateRequest user)
    {
        return await _userHttpClient.PutAsync($"{_controller}", user);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        return await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}

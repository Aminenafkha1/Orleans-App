using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Orleans;
using Softylines.OrleansApp.Abstractions;
using Softylines.OrleansApp.Abstractions.Models;
using Softylines.OrleansApp.Blazor.Models;

namespace Softylines.OrleansApp.Blazor.Services;

public class UserService : IUserService
{
    private readonly IClusterClient _client;
    private readonly IMapper _mapper;

    public UserService(
        IClusterClient client,
        IMapper mapper)
    {
        _client = client;
        _mapper = mapper;
    }


    public async Task<List<User>> GetAllUsers()
    {
        var userGrain = _client.GetGrain<IUsersGrain>(Guid.Empty.ToString());
        var users = await userGrain.GetAllUserDetails();
        return _mapper.Map<List<User>>(users);
    }

    public async Task<bool> AddOrUpdateUserAsync(string userId , User userDetail)
    { 
        var userGrain = _client.GetGrain<IUserGrain>(userId);
        var userProfile = _mapper.Map<Abstractions.Models.UserDetails>(userDetail);
        return await userGrain.CreateOrUpdateUserAsync(userProfile);
    }

    public Task<User> GetUserProfile(string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
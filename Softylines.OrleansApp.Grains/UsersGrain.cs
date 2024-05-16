using Orleans.Runtime;
using Softylines.OrleansApp.Abstractions;
using Softylines.OrleansApp.Abstractions.Models;

namespace Softylines.OrleansApp.Grains;

public class UsersGrain (
    [PersistentState(
        stateName: "Users",
        storageName: "GrainStore")]
    IPersistentState<List<UserDetails>> users): Grain , IUsersGrain  
{
    public Task<List<UserDetails>> GetAllUserDetails()
    {
        return Task.FromResult(users.State);
    }

    
    public async Task<bool> CreateOrUpdateUserAsync(UserDetails userDetail)
    {
        try
        {
            users.State.Add(userDetail); 
            await users.WriteStateAsync(); 
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
using Orleans;
using Orleans.Runtime;
using Softylines.OrleansApp.Abstractions;
using Softylines.OrleansApp.Abstractions.Models;

namespace Softylines.OrleansApp.Grains;

public class UserGrain (
    [PersistentState(
        stateName: "User",
        storageName: "GrainStore")]
    IPersistentState<UserDetails> user,IGrainFactory _grainFactory): Grain , IUserGrain
{
    

    public async Task<bool> CreateOrUpdateUserAsync(UserDetails userDetail)
    {
        try
        {
            user.State  = userDetail;
            await user.WriteStateAsync();
            var userGrain = _grainFactory.GetGrain<IUsersGrain>(string.Empty);
            await userGrain.CreateOrUpdateUserAsync(userDetail); 
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<UserDetails> GetUserDetailsAsync()
    {
        throw new NotImplementedException();
    }
}
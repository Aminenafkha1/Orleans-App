using Softylines.OrleansApp.Abstractions.Models;

namespace Softylines.OrleansApp.Abstractions;

public interface IUsersGrain :   IGrainWithStringKey 
{
    Task<List<UserDetails>> GetAllUserDetails();
    Task<bool> CreateOrUpdateUserAsync(UserDetails userDetail);
}
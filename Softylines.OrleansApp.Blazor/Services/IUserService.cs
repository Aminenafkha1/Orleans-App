using Softylines.OrleansApp.Abstractions.Models;
using Softylines.OrleansApp.Blazor.Models;

namespace Softylines.OrleansApp.Blazor.Services;

public interface IUserService
{ 
    Task<List<User>> GetAllUsers();
    Task<bool> AddOrUpdateUserAsync(string userId, User userDetail);
    
    Task<User> GetUserProfile(string userId); 
    Task RemoveUserAsync(string userId);
}
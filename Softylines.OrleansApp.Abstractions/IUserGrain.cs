using Softylines.OrleansApp.Abstractions.Models;

namespace Softylines.OrleansApp.Abstractions;

public interface IUserGrain : IGrainWithStringKey
{
     

     Task<bool> CreateOrUpdateUserAsync(UserDetails productDetails);

    Task<UserDetails> GetUserDetailsAsync();
}
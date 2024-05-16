using AutoMapper;
using Softylines.OrleansApp.Abstractions.Models;
using Softylines.OrleansApp.Blazor.Models;

namespace Softylines.OrleansApp.Blazor.Data.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, Abstractions.Models.UserDetails>().ReverseMap(); 
    }
}
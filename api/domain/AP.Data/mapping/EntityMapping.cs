using AP.Data.dtos;
using AP.Data.entites;
using AutoMapper;
namespace AP.Data.mapping;

public sealed class EntityMapping:Profile
{
    public EntityMapping()
    {
        CreateMap<Users, UserDto>().ReverseMap();
        CreateMap<UserRole, UserRoleDto>().ReverseMap();
        CreateMap<Departments, DepartmentDto>().ReverseMap();
        CreateMap<Workers, WorkerDto>().ReverseMap();
    }
   
}

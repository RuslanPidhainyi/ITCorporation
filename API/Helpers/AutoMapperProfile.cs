using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmployeeDetails.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.EmployeeDetails.Role));

            CreateMap<CreateUpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeeDetails,
                    opt => opt.MapFrom(src => new EmployeeDetails
                    {
                        Email = src.Email,
                        Role = src.Role
                    }));

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<CreateUpdateProjectDto, Project>();

            CreateMap<Project, ProjectWithEmployeesDto>()
                .ForMember(dest => dest.Employees,
                    opt => opt.MapFrom(src => src.Employee_Projects.Select(ep => ep.Employee)));
        }
    }
}
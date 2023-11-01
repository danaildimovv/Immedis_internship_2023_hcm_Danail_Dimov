using AutoMapper;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Branch, BranchDTO>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<ExperienceLevel, ExperienceLevelDTO>();
            CreateMap<EducationLevel, EducationLevelDTO>();
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();
            CreateMap<Payroll, PayrollDTO>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();
            CreateMap<Country, CountryDTO>();
            CreateMap<Training, TrainingDTO>();
            CreateMap<UserRole, UserRoleDTO>();
            CreateMap<HcmUser, UserDTO>();
        }
    }
}

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
            CreateMap<EmployeeDTO, Employee>();
            
            CreateMap<Branch, BranchDTO>();
            CreateMap<BranchDTO, Branch>();

            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();

            CreateMap<ExperienceLevel, ExperienceLevelDTO>();
            CreateMap<ExperienceLevelDTO, ExperienceLevel>();

            CreateMap<EducationLevel, EducationLevelDTO>();
            CreateMap<EducationLevelDTO, EducationLevel>();
            
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();
            
            CreateMap<Payroll, PayrollDTO>();
            CreateMap<PayrollDTO, Payroll>();
            
            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();
            
            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();
            
            CreateMap<Training, TrainingDTO>();
            CreateMap<TrainingDTO, Training>();

            CreateMap<UserRole, UserRoleDTO>();
            CreateMap<UserRoleDTO, UserRole>();

            CreateMap<HcmUser, UserDTO>();
            CreateMap<UserDTO, HcmUser>();
        }
    }
}

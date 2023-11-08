using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public EmployeeRepository(HcmContext context, IGenericRepository genericRepository) 
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }
        public async Task<Task<bool>> AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var employeeBranch = new EmployeesBranchesHistory()
            {
                EmployeeId = employee.EmployeeId,
                BranchId = employee.BranchId
            };
            await _context.EmployeesBranchesHistory.AddAsync(employeeBranch);

            var employeeJob = new EmployeesJobHistory()
            {
                EmployeeId = employee.EmployeeId,
                JobId = employee.JobId
            };
            await _context.EmployeesJobHistory.AddAsync(employeeJob);

            var projectsTeamHistory = new ProjectsTeamsHistory()
            {
                EmployeeId = employee.EmployeeId,
                ProjectId = employee.ProjectId
            };
            await _context.ProjectsTeamsHistory.AddAsync(projectsTeamHistory);

            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteEmployeeAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            return await _genericRepository.SaveAsync();
        }
    }
}

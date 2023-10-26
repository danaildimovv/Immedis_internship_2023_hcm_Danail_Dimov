using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int JobId { get; set; }

    public int ProjectId { get; set; }

    public int ExperienceLevelId { get; set; }

    public int EducationLevelId { get; set; }

    public int PayrollId { get; set; }

    public int BranchId { get; set; }

    public string Email { get; set; } = null!;

    public int UserId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string EmployeeAddress { get; set; } = null!;

    public int NationalityId { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly DateOfEmployment { get; set; }

    public DateOnly? DateOfLeaving { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<EmployeesBranchesHistory> EmployeesBranchesHistories { get; set; } = new List<EmployeesBranchesHistory>();

    public virtual ICollection<EmployeesJobHistory> EmployeesJobHistories { get; set; } = new List<EmployeesJobHistory>();

    public virtual ExperienceLevel? ExperienceLevel { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Country? Nationality { get; set; }

    public virtual Payroll? Payroll { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<ProjectsTeamsHistory> ProjectsTeamsHistories { get; set; } = new List<ProjectsTeamsHistory>();

    public virtual ICollection<TrainingsHistory> TrainingsHistories { get; set; } = new List<TrainingsHistory>();

    public virtual HcmUser? User { get; set; }  
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ApplicationMVC.Models
{
    public class Employee
    {
        [DisplayName("Employee ID")]
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;

        [DisplayName("Job Title")]
        public int JobId { get; set; }
        public IEnumerable<SelectList>? JobsList { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }
        public IEnumerable<SelectList>? ProjectsList { get; set; }

        [DisplayName("Experience Level")]
        public int ExperienceLevelId { get; set; }
        public IEnumerable<SelectList>? ExperienceLevelsList { get; set; }

        [DisplayName("Education Level")]
        public int EducationLevelId { get; set; }
        public IEnumerable<SelectList>? EducationLevelsList { get; set; }

        [DisplayName("Payroll ID")]
        public int PayrollId { get; set; }
        public IEnumerable<SelectList>? PayrollsList { get; set; }

        [DisplayName("Branch")]
        public int BranchId { get; set; }
        public IEnumerable<SelectList>? BranchesList { get; set; }

        public string Email { get; set; } = null!;

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }

        [DisplayName("Address")]
        public string EmployeeAddress { get; set; } = null!;
        
        [DisplayName("Nationality")]
        public int NationalityId { get; set; }
        public IEnumerable<SelectList>? CountriesList { get; set; }

        public string Gender { get; set; } = null!;

        [DisplayName("Date Of Employment")]
        public DateOnly DateOfEmployment { get; set; }

        [DisplayName("Date Of Leaving")]
        public DateOnly? DateOfLeaving { get; set; }
    }
}
﻿namespace WebAPI.DTO
{
    public class EmployeeDTO
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
    }
}

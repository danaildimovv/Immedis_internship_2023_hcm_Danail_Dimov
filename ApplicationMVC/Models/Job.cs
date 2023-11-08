﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationMVC.Models {

    public class Job
    {
        public int JobId { get; set; }

        public string JobTitle { get; set; } = null!;

        public int DepartmentId { get; set; }
        public IEnumerable<SelectList>? DepartmentsList { get; set; }

        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual ICollection<EmployeesJobHistory> EmployeesJobHistories { get; set; } = new List<EmployeesJobHistory>();
    }
}
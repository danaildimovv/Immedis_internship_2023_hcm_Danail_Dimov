using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = null!;

    public int BranchCountryId { get; set; }

    public virtual Country BranchCountry { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<EmployeesBranchesHistory> EmployeesBranchesHistories { get; set; } = new List<EmployeesBranchesHistory>();
}

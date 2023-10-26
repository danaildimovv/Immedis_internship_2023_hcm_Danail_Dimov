using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}

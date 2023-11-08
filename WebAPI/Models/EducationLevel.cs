using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class EducationLevel
{
    public int EducationLevelId { get; set; }

    public string EducationLevelTitle { get; set; } = null!;
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

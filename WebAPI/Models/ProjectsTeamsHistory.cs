﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class ProjectsTeamsHistory
{
    public int EmployeeProjectId { get; set; }

    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}

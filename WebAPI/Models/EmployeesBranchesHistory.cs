﻿using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class EmployeesBranchesHistory
{
    public int EmployeeBranchId { get; set; }

    public int EmployeeId { get; set; }

    public int BranchId { get; set; }

    public DateOnly DateStarted { get; set; }

    public DateOnly? DateEnded { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}

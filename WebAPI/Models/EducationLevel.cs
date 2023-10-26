using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class EducationLevel
{
    public int EducationLevelId { get; set; }

    public string EducationLevelTitle { get; set; } = null!;
}

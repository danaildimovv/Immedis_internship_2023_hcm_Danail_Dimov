using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string UserRoleTitle { get; set; } = null!;

    public virtual ICollection<HcmUser> HcmUsers { get; set; } = new List<HcmUser>();
}

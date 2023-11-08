using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class HcmUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public int UserRoleId { get; set; }
    public byte[] PasswordSalt { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual UserRole UserRole { get; set; } = null!;
}

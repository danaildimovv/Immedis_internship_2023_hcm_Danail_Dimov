namespace ApplicationMVC.Models { 

    public class HcmUser
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public int UserRoleId { get; set; }

        public virtual Employee? Employee { get; set; }

        public virtual UserRole UserRole { get; set; } = null!;
    }
}
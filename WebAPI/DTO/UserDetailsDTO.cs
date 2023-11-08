namespace WebAPI.DTO
{
    public class UserDetailsDTO
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public int UserRoleId { get; set; }
    }
}

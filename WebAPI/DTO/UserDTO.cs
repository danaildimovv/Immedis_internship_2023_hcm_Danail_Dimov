namespace WebAPI.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public int UserRoleId { get; set; }
    }
}

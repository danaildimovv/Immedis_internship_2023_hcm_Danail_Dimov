namespace WebAPI.DTO
{
    public class JobDTO
    {
        public int JobId { get; set; }

        public string JobTitle { get; set; } = null!;

        public int DepartmentId { get; set; }
    }
}

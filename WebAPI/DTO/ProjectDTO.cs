namespace WebAPI.DTO
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;

        public DateOnly DateStarted { get; set; }

        public DateOnly? DateEnded { get; set; }

        public string? ProjectStatus { get; set; }
    }
}

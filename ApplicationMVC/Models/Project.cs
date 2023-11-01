
namespace ApplicationMVC.Models {

    public partial class Project
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;

        public DateOnly DateStarted { get; set; }

        public DateOnly? DateEnded { get; set; }

        public string? ProjectStatus { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual ICollection<ProjectsTeamsHistory> ProjectsTeamsHistories { get; set; } = new List<ProjectsTeamsHistory>();
    }
}
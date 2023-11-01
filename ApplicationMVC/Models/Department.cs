namespace ApplicationMVC.Models { 
public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
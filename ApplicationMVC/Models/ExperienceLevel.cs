﻿namespace ApplicationMVC.Models { 

    public class ExperienceLevel
    {
        public int ExperienceLevelId { get; set; }

        public string ExperienceLevelTitle { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
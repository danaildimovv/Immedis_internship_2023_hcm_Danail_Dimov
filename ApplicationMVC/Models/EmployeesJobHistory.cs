﻿namespace ApplicationMVC.Models { 
    public class EmployeesJobHistory
    {
        public int EmployeeJobId { get; set; }

        public int EmployeeId { get; set; }

        public int JobId { get; set; }

        public DateOnly DateStarted { get; set; }

        public DateOnly? DateEnded { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public virtual Job Job { get; set; } = null!;
    }
}
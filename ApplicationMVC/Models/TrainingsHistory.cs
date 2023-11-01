namespace ApplicationMVC.Models {
    public partial class TrainingsHistory
    {
        public int EmployeeTrainingId { get; set; }

        public int EmployeeId { get; set; }

        public int TrainingId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public virtual Training Training { get; set; } = null!;
    }
}
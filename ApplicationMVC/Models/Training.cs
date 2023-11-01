namespace ApplicationMVC.Models { 
    public partial class Training
    {
        public int TrainingId { get; set; }

        public string? TrainingTitle { get; set; }

        public string? SkillsCovered { get; set; }

        public virtual ICollection<TrainingsHistory> TrainingsHistories { get; set; } = new List<TrainingsHistory>();
    }
}
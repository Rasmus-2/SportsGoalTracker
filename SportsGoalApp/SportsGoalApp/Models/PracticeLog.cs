namespace SportsGoalApp.Models
{
    public class PracticeLog
    {
        public int Id { get; set; }
        public int? GoalId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int? Duration { get; set; }
        public string? DurationUnit { get; set; }
        public string? Activity { get; set; }
        public string? Notes { get; set; }
        public int? TotalNumber { get; set; }
        public int? SuccessfulNumber { get; set; }
        public float? Percentage { get; set; }
    }
}

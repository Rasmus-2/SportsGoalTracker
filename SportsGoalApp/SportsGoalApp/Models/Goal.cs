namespace SportsGoalApp.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public int? Category {  get; set; }
        public int? GoalNumber { get; set; }
        

    }
}

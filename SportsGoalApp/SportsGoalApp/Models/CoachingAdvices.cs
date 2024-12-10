namespace SportsGoalApp.Models
{
    public class CoachingAdvices
    {
        public int Id { get; set; }
        public string Advice { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

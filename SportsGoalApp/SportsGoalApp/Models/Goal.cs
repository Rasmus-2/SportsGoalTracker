using System.ComponentModel.DataAnnotations;

namespace SportsGoalApp.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Description { get; set; }

        [EnumDataType(typeof(Enums.GoalCategories))]
        public Enums.GoalCategories Category {  get; set; }
        
        public int? GoalNumber { get; set; }
    }
}

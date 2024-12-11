using System.ComponentModel.DataAnnotations;

namespace SportsGoalApp.Enums
{
    public enum GoalCategories
    {
        [Display(Name = "Skill improvement")]
        Skill_improvement,
        [Display(Name = "Fitness")]
        Fitness,
        [Display(Name = "Game sense")]
        Game_sense,
        [Display(Name = "Mental training")]
        Mental_training,
        [Display(Name = "Nutrition and Diet")]
        Nutrition_and_Diet,
        [Display(Name = "Competition preperation")]
        Competition_preperation
    }
}
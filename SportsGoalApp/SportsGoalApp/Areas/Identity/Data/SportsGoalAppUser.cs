using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsGoalApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SportsGoalAppUser class
public class SportsGoalAppUser : IdentityUser
{
    [PersonalData]
    public int Id { get; set; }
    public string? Forename { get; set; }
    public string Surname { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public string? ChosenSport { get; set; }
}


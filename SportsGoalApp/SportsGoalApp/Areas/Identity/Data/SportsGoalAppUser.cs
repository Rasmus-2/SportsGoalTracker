using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsGoalApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SportsGoalAppUser class
public class SportsGoalAppUser : IdentityUser
{
    
    public int Id { get; set; }

    [PersonalData]
    public string? Forename { get; set; }

    [PersonalData]
    public string? Surname { get; set; }

    [PersonalData]
    public int? Age { get; set; }

    //public string? Email { get; set; }
    //public string? Password { get; set; }
    [PersonalData]
    public string? ChosenSport { get; set; }
}


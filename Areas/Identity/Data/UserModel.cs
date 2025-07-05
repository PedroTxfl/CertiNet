using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CertiNet1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserModel class
public class UserModel : IdentityUser
{
    [Required]
    public string Nome { get; set; }

}


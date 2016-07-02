using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace MuscleFellow.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString("D");
        }
        public ApplicationUser(string userName)
        {
            base.UserName = userName;
        }

    }
}

using LibraryApplication.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LibraryApplication.Infrastructure.Identity
{
    public class Config
    {
        public static PasswordOptions GetPasswordOptions()
        {
            return new PasswordOptions()
            {
                RequiredLength = 5,
                RequireDigit = false,
                RequireUppercase = false,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequiredUniqueChars = 0,
            };
        }

        public static IList<IdentityRole<Guid>> GetDefaultRoles()
        {
            var roles = new List<IdentityRole<Guid>>()
            {
                { new IdentityRole<Guid>(DefaultRoleNames.Admin) },
                { new IdentityRole<Guid>(DefaultRoleNames.User) },
            };
            return roles;
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MonitoriaAspCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Cpf { get; set; }
        public override string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public override string Email { get; set; }
        public string Theme { get; set; }
        public string UrlImage { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
        {
            public MyUserClaimsPrincipalFactory(
                UserManager<ApplicationUser> userManager,
                IOptions<IdentityOptions> optionsAccessor)
                    : base(userManager, optionsAccessor)
            {
            }

            protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
            {
                var identity = await base.GenerateClaimsAsync(user);
                identity.AddClaim(new Claim("Cpf", user.Cpf));
                identity.AddClaim(new Claim("Login", user.UserName));
                identity.AddClaim(new Claim("Nome", user.Name));
                identity.AddClaim(new Claim("Genero", user.Gender));
                identity.AddClaim(new Claim("Tema", user.Theme));
                identity.AddClaim(new Claim("ImagemPerfil", user.UrlImage));
                return identity;
            }
        }

    }
}

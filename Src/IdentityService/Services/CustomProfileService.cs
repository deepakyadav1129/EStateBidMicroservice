using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public class CustomProfileService(UserManager<ApplicationUser> userManager) : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject)
                ?? throw new ArgumentException("User not available");

            var existingClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
        {
            new Claim("username", user.UserName!)
        };

            context.IssuedClaims.AddRange(claims);
            context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)!);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
    //public class CustomProfileService : IProfileService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;

    //    public CustomProfileService(UserManager<ApplicationUser> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    //    {
    //        //var user = await _userManager.GetUserAsync(context.Subject);

    //        //var roles = await _userManager.GetRolesAsync(user);

    //        //var roleClaims = roles.Select(role => new Claim("role", role));            
    //        //context.IssuedClaims.AddRange(roleClaims);           
    //        //context.IssuedClaims.AddRange(context.Subject.Claims);
    //    }

    //    public Task IsActiveAsync(IsActiveContext context)
    //    {
    //        context.IsActive = true;
    //        return Task.CompletedTask;
    //    }
    //}
}

﻿namespace NosiYa.Web.Infrastructure.Extensions
{
    using System.Security.Claims;
    using static Common.SeedingConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier)?.ToUpper() ?? null;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}

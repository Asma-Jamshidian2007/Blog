using System;
using System.Security.Claims;

namespace Blog_System.CoreLayer.Utilities
{
    public static class UserUtil
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                userIdClaim = principal.FindFirst("sub")?.Value;
            }

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new InvalidOperationException("User ID is not valid or not found in the claims.");
            }

            return userId;
        }
    }
}

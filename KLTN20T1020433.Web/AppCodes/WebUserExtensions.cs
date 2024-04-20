using System.Security.Claims;

namespace KLTN20T1020433.Web.AppCodes
{
    public static class WebUserExtensions
    {
        /// <summary>
        /// Giải mã lại thông tin đã được mã hóa
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static WebUserData? GetUserData(this ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
                    return null;

                var userData = new WebUserData();

                userData.UserId = principal.FindFirstValue(nameof(userData.UserId));
                userData.DisplayName = principal.FindFirstValue(nameof(userData.DisplayName));
                userData.Email = principal.FindFirstValue(nameof(userData.Email));

                userData.ClientIP = principal.FindFirstValue(nameof(userData.ClientIP));
                userData.SessionId = principal.FindFirstValue(nameof(userData.SessionId));

                userData.Role = principal.FindFirstValue(nameof(userData.Role));               

                return userData;
            }
            catch
            {
                return null;
            }

        }
    }
}

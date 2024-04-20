using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace KLTN20T1020433.Web.AppCodes
{
    public class WebUserData
    {
        public string? UserId { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? ClientIP { get; set; }
        public string? SessionId { get; set; }
        public string? Role { get; set; }

        /// <summary>
        /// Thông tin người dùng dưới dạng danh sách các Claim
        /// </summary>
        /// <returns></returns>
        private List<Claim> Claims
        {
            get
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(nameof(UserId), UserId ?? ""),
                    new Claim(nameof(DisplayName), DisplayName ?? ""),
                    new Claim(nameof(Email), Email ?? ""),
                    new Claim(nameof(ClientIP), ClientIP ?? ""),
                    new Claim(nameof(SessionId), SessionId ?? ""),
                    new Claim(nameof(Role), Role ?? ""),
                };
                return claims;
            }
        }

        /// <summary>
        /// Tạo Principal dựa trên thông tin của người dùng
        /// </summary>
        /// <returns></returns>
        public ClaimsPrincipal CreatePrincipal()
        {
            var claimIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

            return claimPrincipal;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KLTN20T1020433.Web.AppCodes
{
    public class TeacherOnlyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User.GetUserData(); // Lấy dữ liệu người dùng

            // Kiểm tra vai trò của người dùng
            if (user.Role != Constants.TEACHER_ROLE)
            {
                // Nếu người dùng không phải là sinh viên, chuyển hướng đến trang không có quyền truy cập
                filterContext.Result = new RedirectResult("~/Account/AccessDenied");
            }

            base.OnActionExecuting(filterContext);
        }
    }

}

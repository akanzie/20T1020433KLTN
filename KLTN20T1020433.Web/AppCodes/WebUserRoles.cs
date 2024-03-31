﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KLTN20T102433.Application.AppCodes
{
    public class WebUserRoles
    {
        /// <summary>
        /// Lấy danh sách thông tin các Role dựa vào các hằng được định nghĩa trong lớp này
        /// </summary>
        public static List<WebUserRole> ListOfRoles
        {
            get
            {
                List<WebUserRole> listOfRoles = new List<WebUserRole>();

                Type type = typeof(WebUserRoles);
                var listFields = type.GetFields(BindingFlags.Public
                                                | BindingFlags.Static
                                                | BindingFlags.FlattenHierarchy)
                               .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string));

                foreach (var role in listFields)
                {
                    string? roleName = role.GetRawConstantValue() as string;
                    if (roleName != null)
                    {
                        DisplayAttribute? attribute = role.GetCustomAttribute<DisplayAttribute>();

                        if (attribute != null)
                            listOfRoles.Add(new WebUserRole(roleName, attribute.Name ?? roleName));
                        else
                            listOfRoles.Add(new WebUserRole(roleName, roleName));
                    }
                }

                return listOfRoles;
            }
        }
    }
    [Display(Name = "Quản trị hệ thống")]
    public const string Administrator = "admin";

    [Display(Name = "Nhân viên")]
    public const string Employee = "employee";

    [Display(Name = "Khách hàng")]
    public const string Customer = "customer";

}

using _20T1020433KLTN.BussinessLayers;
using _20T1020433KLTN.Domain.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _20T1020433KLTN.Application.AppCodes
{
    public class SelectListHelper
    {
        public static List<SelectListItem> GetTestStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = TestStatus.All.ToString(),
                Text = "-- Tất cả --"
            });

            foreach (TestStatus item in Enum.GetValues(typeof(TestStatus)))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text = Utils.GetTestStatusDisplayName(item)
                });
            }

            return list;
        }
    }
}

using KLTN20T1020433.BusinessLayers;
using KLTN20T1020433.DomainModels.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KLTN20T1020433.Web.AppCodes
{
    public class SelectListHelper
    {
        public static List<SelectListItem> GetTestStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
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
        public static List<SelectListItem> GetTestType()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Kỳ thi/Bài kiểm tra --"
            });

            foreach (TestType item in Enum.GetValues(typeof(TestType)))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text = Utils.GetTestTypeDisplayName(item)
                });
            }

            return list;
        }
        public static List<SelectListItem> GetSubmissionStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Tất cả --"
            });

            foreach (SubmissionStatus item in Enum.GetValues(typeof(SubmissionStatus)))
            {
                list.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text = Utils.GetSubmissionStatusDisplayName(item)
                });
            }

            return list;
        }
    }
}

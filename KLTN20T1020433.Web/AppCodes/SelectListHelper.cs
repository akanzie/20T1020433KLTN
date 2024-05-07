using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Enum;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KLTN20T1020433.Web.AppCodes
{
    public class SelectListHelper
    {
        public static List<SelectListItem> GetTestStatusForStudent()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Tất cả --"
            });

            foreach (TestStatus item in Enum.GetValues(typeof(TestStatus)))
            {
                if (Utils.GetTestStatusDisplayNameForStudent(item) != "")
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.ToString(),
                        Text = Utils.GetTestStatusDisplayNameForStudent(item)
                    });
                }
            }

            return list;
        }
        public static List<SelectListItem> GetTestStatusForTeacher()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Trạng thái --"
            });
            foreach (TestStatus item in Enum.GetValues(typeof(TestStatus)))
            {
                if (Utils.GetTestStatusDisplayNameForTeacher(item) != "")
                {
                    list.Add(new SelectListItem()
                    {
                        Value = item.ToString(),
                        Text = Utils.GetTestStatusDisplayNameForTeacher(item)
                    });
                }
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

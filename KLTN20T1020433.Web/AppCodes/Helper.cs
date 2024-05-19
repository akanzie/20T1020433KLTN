using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Enum;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KLTN20T1020433.Web.AppCodes
{
    public class Helper
    {
        public static List<string> GetScholastics()
        {
            List<string> scholastics = new List<string>();
            int startYear = 2015;
            int currentYear = DateTime.Now.Year;
            int endYear = DateTime.Now.Month >= 9 ? currentYear + 1 : currentYear;

            for (int year = startYear; year < endYear; year++)
            {
                scholastics.Add($"{year}-{year + 1}");
            }
            return scholastics;
        }

        public static List<SelectListItem> GetTestStatusForStudent()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "Tất cả"
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
                Text = "Trạng thái"
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
                Text = "Kỳ thi/Bài kiểm tra"
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
                Text = "Tất cả"
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
        public static string GetIconClassFromFileExtension(string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileExtension))
                return ""; // Or whatever default behavior you prefer for empty extensions

            fileExtension = fileExtension.TrimStart('.').ToLower();
            string defaultIcon = $"<i class=\"bi bi-filetype-{fileExtension}\"></i>";

            switch (fileExtension)
            {
                case ".pdf":
                    return "<i class=\"bi bi-filetype-pdf\"></i>";
                case ".doc":
                case ".docx":
                    return "<i class=\"bi bi-file-word\"></i>";
                case ".ppt":
                case ".pptx":
                    return "<i class=\"bi bi-file-ppt\"></i>";
                case ".txt":
                    return "<i class=\"bi bi-file-text\"></i>";
                case ".zip":
                    return "<i class=\"bi bi-file-zip\"></i>";
                case ".rar":
                    return "<i class=\"bi bi-file-rar\"></i>";
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return "<i class=\"bi bi-file-image\"></i>";
                default:
                    return defaultIcon; // Default icon for other file types
            }
        }


    }
}

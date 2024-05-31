using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Services
{
    public static class Utils
    {
        public static string GetTestStatusDisplayNameForStudent(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.Upcoming:
                    return "Chưa bắt đầu";
                case TestStatus.Ongoing:
                    return "Đang diễn ra";
                case TestStatus.Finished:
                    return "Đã kết thúc";
                default:
                    return "";
            }
        }
        public static string GetTestStatusDisplayNameForTeacher(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.Upcoming:
                    return "Chưa bắt đầu";
                case TestStatus.Ongoing:
                    return "Đang diễn ra";
                case TestStatus.Finished:
                    return "Đã kết thúc";
                case TestStatus.Creating:
                    return "Đang tạo";
                case TestStatus.Canceled:
                    return "Đã bị hủy";
                default:
                    return "";
            }
        }
        public static string GetTestTypeDisplayName(TestType status)
        {
            switch (status)
            {
                case TestType.Exam:
                    return "Kỳ thi";
                case TestType.Quiz:
                    return "Bài kiểm tra";
                default:
                    return "";
            }
        }
        public static string GetSubmissionStatusDisplayName(SubmissionStatus status)
        {
            switch (status)
            {
                case SubmissionStatus.NotSubmitted:
                    return "Chưa nộp bài";
                case SubmissionStatus.Absent:
                    return "Không nộp bài (Thiếu)";
                case SubmissionStatus.LateSubmission:
                    return "Đã nộp muộn";
                case SubmissionStatus.Submitted:
                    return "Đã nộp";
                case SubmissionStatus.PendingProcessing:
                    return "Đang chờ xử lý";
                default:
                    return "";
            }
        }
        public static bool CheckIPAddress(string ipAddress)
        {
            if (ipAddress == null) return false;
            else if (ipAddress == "")
                return false;
            return true;
        }
        public static string CalculateSignature(string appId, string secretKey, string time)
        {
            string data = $"{appId}{secretKey}{time}";

            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public static (string FirstName, string LastName) ParseStudentName(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
            {
                return ("Unknown", "Unknown");
            }

            var names = studentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 1)
            {
                return ("Unknown", names[0]); // If there's only one name, treat it as LastName
            }

            var firstName = names[^1]; // Last element is FirstName
            var lastName = string.Join(' ', names.Take(names.Length - 1)); // Join all but the last as LastName
            return (firstName, lastName);
        }
        public static (string SchoolYear, int Semester) ParseSemester(string semesterString)
        {
            if (string.IsNullOrWhiteSpace(semesterString))
            {
                return ("", 0);
            }

            var parts = semesterString.Split('.');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int semester))
            {
                return ("", 0);
            }

            string schoolYear = parts[0];
            return (schoolYear, semester);
        }

    }
}

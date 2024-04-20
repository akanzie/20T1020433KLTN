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
        public static string GetTestStatusDisplayName(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.Upcoming:
                    return "Chưa bắt đầu";
                case TestStatus.Ongoing:
                    return "Đang diễn ra";
                case TestStatus.Finished:
                    return "Đã kết thúc";
                case TestStatus.Canceled:
                    return "Đã bị hủy";
                case TestStatus.Creating:
                    return "Đang tạo";
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
        public static bool CheckIPAddress(IPAddress ipAddress)
        {
            if (ipAddress == null) return false;
            else if (ipAddress.Address != 0)
                return false;
            return true;
        }
        public static bool CheckIPAddressExists(IPAddress ipAddress)
        {
            throw new NotImplementedException();
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
    }
}

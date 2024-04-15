using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using System.Net;

namespace KLTN20T1020433.Web.AppCodes
{
    public static class Utils
    {
        public static bool CheckIPAddress(IPAddress ipAddress)
        {
            if (ipAddress == null) return false;
            else if (ipAddress.Address != 0)
                return false;
            return true;
        }
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

        internal static bool CheckIPAddressExists(IPAddress ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}

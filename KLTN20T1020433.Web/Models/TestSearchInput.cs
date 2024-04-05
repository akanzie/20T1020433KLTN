using KLTN20T1020433.Web.AppCodes;
using KLTN20T1020433.DomainModels.Enum;

namespace KLTN20T1020433.Web.Models
{
    public class TestSearchInput : PaginationSearchInput
    {
        public string StudentId { get; set; };
        public TestType? Type { get; set; } = null;
        /// <summary>
        /// Trạng thái của đơn hàng cần tìm
        /// </summary>
        public TestStatus? Status { get; set; } = null;



        /// <summary>
        /// Lấy thời điểm bắt đầu dựa vào DateRange
        /// </summary>
        public DateTime? FromTime { get; set; } = null;



        /// <summary>
        /// Lấy thời điểm kết thúc dựa vào DateRange
        /// (thời điểm kết thúc phải là cuối ngày)
        /// </summary>
        public DateTime? ToTime { get; set; } = null;
    }
}

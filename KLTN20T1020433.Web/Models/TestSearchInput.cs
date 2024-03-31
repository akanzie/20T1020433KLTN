using KLTN20T102433.Application.AppCodes;
using KLTN20T102433.Domain.Enum;

namespace KLTN20T102433.Application.Models
{
    public class TestSearchInput : PaginationSearchInput
    {
        
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

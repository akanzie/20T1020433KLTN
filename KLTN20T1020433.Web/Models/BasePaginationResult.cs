using KLTN20T1020433.DomainModels.Entities;

namespace KLTN20T1020433.Web.Models
{
    public abstract class BasePaginationResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; } = "";
        public int RowCount { get; set; }
        
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int c = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    c += 1;

                return c;
            }

        }
    }

    /// <summary>
    /// Kết quả tìm kiếm và lấy danh sách khách hàng
    /// </summary>
    public class StudentSearchResult : BasePaginationResult
    {
        public List<Student>? Data { get; set; }
    }   


}

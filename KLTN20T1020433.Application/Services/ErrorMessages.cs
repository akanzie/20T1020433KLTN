namespace KLTN20T1020433.Application.Services
{
    public class ErrorMessages
    {
        public const string GeneralError = "Có lỗi xảy ra trong quá trình xử lý yêu cầu của bạn. Vui lòng thử lại sau.";
        public const string ConnectionError = "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra lại kết nối mạng của bạn hoặc liên hệ với quản trị viên hệ thống.";
        public const string RequestNotCompleted = "Yêu cầu của bạn không thể hoàn thành do sự cố trong cơ sở dữ liệu. Xin lỗi về sự bất tiện này. Chúng tôi sẽ khắc phục vấn đề càng sớm càng tốt.";
        public const string RetryLater = "Vui lòng thử lại sau và chắc chắn rằng bạn đã nhập thông tin đúng đắn.";
        public const string NoFilesUploaded = "Không có tệp nào được tải lên.";
        public const string InvalidOrLargeFile = "Tệp không hợp lệ hoặc có kích thước quá lớn.";
        public const string FileUploadError = "Có lỗi khi lưu tệp.";
        public const string FileNotFound = "Không tìm thấy tệp.";
        public const string FileUploadErrorGeneric = "Có lỗi khi tải lên tệp.";
        public const string SubmissionNotFound = "Không tìm thấy bài nộp.";
        public const string CannotSubmitWithoutUpload = "Bạn không thể nộp bài khi chưa tải bài nộp lên.";
        public const string CannotSubmit = "Bạn không thể nộp bài.";
        public const string CannotCancelSubmit = "Bạn không thể hủy nộp bài.";
        public const string CannotRemoveFile = "Bạn không thể xóa tệp.";
        public const string TestNotFound = "Không tìm thấy kỳ thi.";
        public const string CannotDeleteFinishedTest = "Không thể xóa kỳ thi khi kỳ thi đã kết thúc.";
        public const string CannotEditFinishedTest = "Không thể chỉnh sửa kỳ thi khi kỳ thi đã kết thúc.";
        public const string InvalidIPAddress = "Địa chỉ IP không hợp lệ. Bạn không được phép nộp bài.";
        public const string SubmissionTimeExceeded = "Đã quá thời gian cho phép nộp bài. Bạn không được phép nộp bài.";
        public const string ListStudentsIsEmpty = "Danh sách sinh viên tham gia rỗng.";
    }
}


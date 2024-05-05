using KLTN20T1020433.Application.Commands.StudentCommands.Delete;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using MediatR;

namespace KLTN20T1020433.Web.Areas.Teacher.Commands.Delete
{
    public class DeleteTestCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, string>
    {
        private readonly ITestRepository _testDB;

        public DeleteTestCommandHandler(ITestRepository testDB)
        {
            _testDB = testDB;
        }
        public async Task<string> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _testDB.GetById(request.Id);
            if (test == null)
            {
                return "Không tìm thấy kỳ thi";
            }
            if (test.Status == TestStatus.Finished)
            {
                return "Không thể xóa kỳ thi khi kỳ thi đã kết thúc";
            }
            if (await _testDB.Delete(request.Id))
            {
                return $"Xóa kỳ thi: {test.Title} thành công";
            }
            return "Có lỗi xảy ra. Vui lòng thử lại sau.";

        }
    }
}

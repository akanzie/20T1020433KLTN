using AutoMapper;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Application.DTOs.StudentDTOs;
using KLTN20T1020433.Domain.Test;

namespace KLTN20T1020433.Application.Queries.StudentQueries
{
    public class GetSubmissionByStudentIdAndTestIdQuery : IRequest<GetSubmissionResponse?>
    {
        public string StudentId { get; set; }
        public int TestId { get; set; }
    }
    public class GetSubmissionByStudentIdAndTestIdQueryHandler : IRequestHandler<GetSubmissionByStudentIdAndTestIdQuery, GetSubmissionResponse?>
    {
        private readonly ISubmissionRepository _submissionDB;
        private readonly ITestRepository _testDB;
        private readonly IMapper _mapper;

        public GetSubmissionByStudentIdAndTestIdQueryHandler(ISubmissionRepository submissionDB, ITestRepository testDB, IMapper mapper)
        {
            _submissionDB = submissionDB;
            _testDB = testDB;
            _mapper = mapper;
        }

        public async Task<GetSubmissionResponse?> Handle(GetSubmissionByStudentIdAndTestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var submission = await _submissionDB.GetByTestIdAndStudentId(request.TestId, request.StudentId);
                if (submission != null)
                {
                    var test = await _testDB.GetById(request.TestId);
                    GetSubmissionResponse submissionResponse = _mapper.Map<GetSubmissionResponse>(submission);
                    if (submissionResponse.Status == SubmissionStatus.NotSubmitted && !test.CanSubmitLate && test.EndTime < DateTime.Now)
                    {
                        submission.Status = SubmissionStatus.Absent;
                        await _submissionDB.Update(submission);
                        submissionResponse.Status = SubmissionStatus.Absent;
                    }
                    submissionResponse.StatusDisplayName = Utils.GetSubmissionStatusDisplayName(submission.Status);
                    return submissionResponse;
                }
                return null;
            }
            catch (Exception ex)
            {                
                Console.WriteLine("Đã xảy ra lỗi khi xử lý yêu cầu lấy bài nộp: " + ex.Message);
                throw;
            }
        }

    }
}

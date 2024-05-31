using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByExamIdQuery : GetTokenResponse, IRequest<IEnumerable<GetStudentResponse>>
    {
        public string ExamId { get; set; }
    }
    public class GetStudentsByExamIdQueryHandler : IRequestHandler<GetStudentsByExamIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsByExamIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByExamIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //string endpoint = $"teacher-services/v1/get-teaching-students?courseId={request.CourseId}";
                //string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                //if (jsonResponse != null)
                //{
                //    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                //    IEnumerable<GetStudentResponse> students = JsonConvert.DeserializeObject<List<GetStudentResponse>>(responseData.Data.ToString())!;
                //    return students;
                //}
                return new List<GetStudentResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy danh sách sinh viên của lớp học phần: {ex.Message}");
                throw;
            }
        }
    }
}

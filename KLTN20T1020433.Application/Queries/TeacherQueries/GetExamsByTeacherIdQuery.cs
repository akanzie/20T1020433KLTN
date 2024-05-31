using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetExamScheduleQuery : IRequest<IEnumerable<GetExamScheduleResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string Semester { get; set; }
    }
    public class GetExamScheduleQueryHandler : IRequestHandler<GetExamScheduleQuery, IEnumerable<GetExamScheduleResponse>>
    {
        private readonly ApiService _apiService;

        public GetExamScheduleQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetExamScheduleResponse>> Handle(GetExamScheduleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"teacher-services/v1/get-exam-schedule?semester={request.Semester}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    IEnumerable<GetExamScheduleResponse> courses = JsonConvert.DeserializeObject<List<GetExamScheduleResponse>>(responseData.Data.ToString())!;
                    return courses;
                }
                else
                {
                    return new List<GetExamScheduleResponse>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy lịch thi kỳ thi: {ex.Message}");
                throw;
            }
        }
    }
}

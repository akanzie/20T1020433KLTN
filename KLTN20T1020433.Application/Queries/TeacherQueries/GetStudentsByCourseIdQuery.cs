using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByCourseIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public string CourseId { get; set; }
    }
    public class GetStudentsByCourseIdQueryHandler : IRequestHandler<GetStudentsByCourseIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsByCourseIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            string endpoint = "account/v1/students";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            if (jsonResponse != null)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                IEnumerable<GetStudentResponse> students = JsonConvert.DeserializeObject<GetStudentResponse>(responseData.Data.ToString())!;
                return students;
            }
            return new List<GetStudentResponse>();
        }
    }
}

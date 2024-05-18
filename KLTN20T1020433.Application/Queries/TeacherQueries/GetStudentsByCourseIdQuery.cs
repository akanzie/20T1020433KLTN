using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByCourseIdQuery : GetTokenResponse, IRequest<IEnumerable<GetStudentResponse>>
    {       
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
            var students = new List<GetStudentResponse>();
            int j = 400;

            for (int i = 0; i < 34; i++)  // Loop 34 times to cover 20T1020400 to 20T1020433
            {
                var student = new GetStudentResponse
                {
                    StudentId = $"20T1020{j}",
                    FirstName = $"Kiệt{j}",
                    LastName = "Châu Anh",
                    Email = $"20T1020{j}@husc.edu.vn"
                };
                students.Add(student);
                j++;
            }
            return students;
        }
        /*public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            string endpoint = " $"api/v1/student/course/{request.CourseId}";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            if (jsonResponse != null)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                IEnumerable<GetStudentResponse> students = JsonConvert.DeserializeObject<GetStudentResponse>(responseData.Data.ToString())!;
                return students;
            }
            return new List<GetStudentResponse>();
        }*/
    }
}

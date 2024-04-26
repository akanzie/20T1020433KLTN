using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByExamIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public string CourseId { get; set; }
    }
    public class GetStudentsByExamIdQueryHandler : IRequestHandler<GetStudentsByCourseIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsByExamIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
           var students = new List<GetStudentResponse>();
            int j = 0;
            for (int i = 0; i < 9; i++)
            {
                var student = new GetStudentResponse
                {
                    StudentId = $"20T102000{j++}",
                    FirstName = $"Kiệt{j++}",
                    LastName = $"Châu Anh",
                    Email = $"20T102000{j++}@husc.edu.vn"
                };
                students.Add(student);
            }
            return students;
        }
        /*public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
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
        }*/
    }
}

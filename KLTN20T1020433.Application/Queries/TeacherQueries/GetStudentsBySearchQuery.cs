using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsBySearchQuery : PaginationSearchInput, IRequest<IEnumerable<GetStudentResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }       
    }
    public class GetStudentsBySearchQueryHandler : IRequestHandler<GetStudentsBySearchQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsBySearchQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsBySearchQuery request, CancellationToken cancellationToken)
        {
            var students = new List<GetStudentResponse>();
            int j = 0;
            for (int i = 0; i < 100; i++)
            {
                var student = new GetStudentResponse
                {
                    StudentId = $"20T102000{j}",
                    FirstName = $"Kiệt{j}",
                    LastName = $"Châu Anh",
                    Email = $"20T102000{j}@husc.edu.vn"
                };
                j++;
                if (student.FirstName.Contains(request.SearchValue) || student.LastName.Contains(request.SearchValue) || student.StudentId.Contains(request.SearchValue))
                {
                    students.Add(student);
                }
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

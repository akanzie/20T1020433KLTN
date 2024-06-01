using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsBySearchQuery : PaginationSearchInput, IRequest<IEnumerable<GetStudentResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public IEnumerable<GetCourseResponse> Courses { get; set; }
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
            try
            {
                List<GetStudentResponse> studentSearchings = new List<GetStudentResponse>();
                foreach (var item in request.Courses)
                {
                    string endpoint = $"undergraduate-services/v1/course/get-students?courseId={item.CourseId}";
                    string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                    if (jsonResponse != null)
                    {
                        var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                        IEnumerable<GetStudentResponse> students = JsonConvert.DeserializeObject<IEnumerable<GetStudentResponse>>(responseData.Data.ToString());
                        foreach (var student in students)
                        {
                            if (student.FirstName.Contains(request.SearchValue, StringComparison.OrdinalIgnoreCase) || student.LastName.Contains(request.SearchValue, StringComparison.OrdinalIgnoreCase))
                            {
                                studentSearchings.Add(student);
                            }
                        }
                    }
                }
                return studentSearchings;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Đã xảy ra ngoại lệ khi tìm kiếm sinh viên có đăng ký học phần: {ex.Message}");
                throw;
            }
        }
    }
}

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
    public class GetCoursesInSemesterQuery : IRequest<IEnumerable<GetCourseResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string Semester { get; set; }
        public string ModuleId { get; set; } = "";
    }
    public class GetCoursesInSemesterQueryHandler : IRequestHandler<GetCoursesInSemesterQuery, IEnumerable<GetCourseResponse>>
    {
        private readonly ApiService _apiService;

        public GetCoursesInSemesterQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetCourseResponse>> Handle(GetCoursesInSemesterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"undergraduate-services/v1/course/list?semester={request.Semester}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                if (jsonResponse != null)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var data = JsonConvert.DeserializeObject<List<LopHocPhan>>(responseData.Data.ToString())!;
                    List<GetCourseResponse> courses = new List<GetCourseResponse>();
                    foreach (var courseData in data)
                    {
                        if (courseData.MaHocPhan == request.ModuleId || request.ModuleId == "")
                        {
                            courses.Add(new GetCourseResponse { CourseId = courseData.MaLopHocPhan, CourseName = courseData.TenLopHocPhan });
                        }
                    }
                    return courses;
                }
                return new List<GetCourseResponse>();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy danh sách lớp học phần: {ex.Message}");
                throw;
            }
        }
    }
}

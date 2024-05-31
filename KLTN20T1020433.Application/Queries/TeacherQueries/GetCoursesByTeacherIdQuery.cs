using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetCoursesByTeacherIdQuery : IRequest<IEnumerable<GetCourseResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string Semester { get; set; }
        public string ModuleId { get; set; } = "";
    }
    public class GetCoursesByTeacherIdQueryHandler : IRequestHandler<GetCoursesByTeacherIdQuery, IEnumerable<GetCourseResponse>>
    {
        private readonly ApiService _apiService;

        public GetCoursesByTeacherIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetCourseResponse>> Handle(GetCoursesByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"teacher-services/v1/get-teaching-courses?semester={request.Semester}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                if (jsonResponse != null)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var data = JsonConvert.DeserializeObject<List<CourseData>>(responseData.Data.ToString())!;
                    List<GetCourseResponse> courses = new List<GetCourseResponse>();
                    foreach (var courseData in data)
                    {
                        if (courseData.LopHocPhan.MaHocPhan == request.ModuleId || request.ModuleId == "")
                        {
                            GetCourseResponse response = new GetCourseResponse
                            {
                                CourseId = courseData.LopHocPhan.MaLopHocPhan,
                                CourseName = courseData.LopHocPhan.TenLopHocPhan,
                                StudentCount = courseData.LopHocPhan.SoSinhVienDaDuyet 
                            };
                            courses.Add(response);
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
    public class CourseData
    {
        public string MaGiangVien { get; set; }
        public string MaSoTayGiangVien { get; set; }
        public int TongSoGio { get; set; }
        public int SoGioLyThuyet { get; set; }
        public int SoGioBaiTap { get; set; }
        public int SoGioThaoLuan { get; set; }
        public int SoGioThucHanh { get; set; }
        public int SoGioTuHoc { get; set; }
        public LopHocPhan LopHocPhan { get; set; }
    }

    public class LopHocPhan
    {
        public string MaLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; }
        public string MaHocPhan { get; set; }
        public string TenHocPhan { get; set; }
        public int SoTinChi { get; set; }
        public int SoSinhVienDaDuyet { get; set; }
        // Add other properties as needed
    }

}

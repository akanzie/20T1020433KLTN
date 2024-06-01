using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;
using System.Globalization;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetExamScheduleQuery : IRequest<IEnumerable<GetExamScheduleResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string Semester { get; set; }
        public string ModuleId { get; set; }
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
                    var data = JsonConvert.DeserializeObject<List<ExamSchedule>>(responseData.Data.ToString())!;
                    List<GetExamScheduleResponse> examSchedules = new List<GetExamScheduleResponse>();
                    foreach (var item in data)
                    {
                        if (item.MaHocPhan == request.ModuleId || request.ModuleId == "")
                        {
                            DateTime ngayThi = DateTime.ParseExact(item.NgayThi, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);                            
                            string ngayThiFormatted = ngayThi.ToString("dd/MM/yyyy");                        
                            string examDate = $"{item.GioThi} ngày {ngayThiFormatted}";
                            examSchedules.Add(new GetExamScheduleResponse { CourseName = item.TenLopHocPhan, ExamDate = examDate, ExamDuration = item.ThoiGianThi });
                        }
                    }
                    return examSchedules;                    
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
    public class ExamSchedule
    {
        public string MaLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; }
        public string MaHocPhan { get; set; }
        public int LanThi { get; set; }
        public string MaHinhThucThi { get; set; }
        public string TenHinhThucThi { get; set; }
        public string NgayThi { get; set; }
        public string GioThi { get; set; }
        public int ThoiGianThi { get; set; }
    }
}

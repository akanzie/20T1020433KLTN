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
    public class GetModulesQuery : IRequest<IEnumerable<Module>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string Semester { get; set; }
    }
    public class GetModulesQueryHandler : IRequestHandler<GetModulesQuery, IEnumerable<Module>>
    {
        private readonly ApiService _apiService;

        public GetModulesQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<Module>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"teacher-services/v1/get-teaching-courses?semester={request.Semester}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                if (jsonResponse != null)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var data = JsonConvert.DeserializeObject<List<CourseData>>(responseData.Data.ToString())!;
                    List<Module> modules = new List<Module>();
                    foreach (var courseData in data)
                    {
                        string maHocPhan = courseData.LopHocPhan.MaHocPhan;
                        if (!modules.Exists(item => item.ModuleId == maHocPhan))
                        {
                            var module = new Module
                            {
                                ModuleId = maHocPhan,
                                ModuleName = courseData.LopHocPhan.TenHocPhan

                            };
                            modules.Add(module);
                        }
                    }
                    return modules;
                }
                return new List<Module>();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy danh sách học phần: {ex.Message}");
                throw;
            }
        }
    }
    public class Module
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}

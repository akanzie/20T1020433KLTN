using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetModuleByIdQuery : IRequest<Module>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string ModuleId { get; set; }
    }
    public class GetModuleByIdQueryHandler : IRequestHandler<GetModuleByIdQuery, Module>
    {
        private readonly ApiService _apiService;

        public GetModuleByIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<Module> Handle(GetModuleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"common/v1/module/get?moduleId={request.ModuleId}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                if (jsonResponse != null)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    var module = JsonConvert.DeserializeObject<Module>(responseData.Data.ToString())!;
                    return module;
                }
                return new Module();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy danh sách học phần: {ex.Message}");
                throw;
            }
        }
    }
}

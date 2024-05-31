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
    public class GetSchoolYearQuery : IRequest<IEnumerable<SchoolYear>>
    {

    }
    public class GetSchoolYearQueryHandler : IRequestHandler<GetSchoolYearQuery, IEnumerable<SchoolYear>>
    {
        private readonly ApiService _apiService;

        public GetSchoolYearQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<SchoolYear>> Handle(GetSchoolYearQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = "common/v1/school-year/list";
                string jsonResponse = await _apiService.SendAsync(endpoint, null, null);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    if (responseData?.Data != null)
                    {
                        var schoolYear = JsonConvert.DeserializeObject<IEnumerable<SchoolYear>>(responseData.Data.ToString());
                        return schoolYear ?? new List<string>();
                    }
                }
                return new List<SchoolYear>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy năm học: {ex.Message}");
                throw;
            }

        }

    }
    public class SchoolYear
    {
        public string NamHoc { get; set; }
    }
}

using KLTN20T1020433.Application.DTOs.StudentDTOs;
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
    public class GetCoursesByTeacherIdQuery : IRequest<IEnumerable<GetCourseResponse>>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public string TeacherId { get; set; }
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
            string endpoint = "account/v1/courses";
            string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
            if (jsonResponse != null)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                IEnumerable<GetCourseResponse> courses = JsonConvert.DeserializeObject<GetCourseResponse>(responseData.Data.ToString())!;
                return courses;
            }
            return new List<GetCourseResponse>();
        }
    }
}

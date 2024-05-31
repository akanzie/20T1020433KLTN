﻿using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByCourseIdQuery : IRequest<IEnumerable<GetStudentResponse>>
    {
        public string Token { get; set; }
        public string Signature { get; set; }
        public string CourseId { get; set; }

    }
    public class GetStudentsByCourseIdQueryHandler : IRequestHandler<GetStudentsByCourseIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsByCourseIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"teacher-services/v1/get-teaching-students?courseId={request.CourseId}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.Token, request.Signature);
                if (jsonResponse != null)
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    IEnumerable<GetStudentResponse> students = JsonConvert.DeserializeObject<List<GetStudentResponse>>(responseData.Data.ToString())!;
                    return students;
                }
                return new List<GetStudentResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ khi lấy danh sách sinh viên của lớp học phần: {ex.Message}");
                throw;
            }
        }
    }
}

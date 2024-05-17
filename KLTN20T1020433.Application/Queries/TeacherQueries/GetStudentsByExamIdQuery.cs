﻿using KLTN20T1020433.Application.DTOs;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Student;
using MediatR;
using Newtonsoft.Json;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentsByExamIdQuery : GetTokenResponse, IRequest<IEnumerable<GetStudentResponse>>
    {       
        public string ExamId { get; set; }
    }
    public class GetStudentsByExamIdQueryHandler : IRequestHandler<GetStudentsByExamIdQuery, IEnumerable<GetStudentResponse>>
    {
        private readonly ApiService _apiService;

        public GetStudentsByExamIdQueryHandler(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByExamIdQuery request, CancellationToken cancellationToken)
        {
           var students = new List<GetStudentResponse>();
            int j = 0;
            for (int i = 0; i < 9; i++)
            {
                var student = new GetStudentResponse
                {
                    StudentId = $"20T102000{j++}",
                    FirstName = $"Kiệt{j++}",
                    LastName = $"Châu Anh",
                    Email = $"20T102000{j++}@husc.edu.vn"
                };
                students.Add(student);
            }
            return students;
        }
        /*public async Task<IEnumerable<GetStudentResponse>> Handle(GetStudentsByExamIdQuery request, CancellationToken cancellationToken)
        {
            string endpoint = " $"api/v1/student/exam/{request.ExamId}";
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

using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Application.Services;
using KLTN20T1020433.Domain.Submission;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetStudentByIdQuery : IRequest<GetStudentResponse?>
    {
        public GetTokenResponse GetTokenResponse { get; set; }
        public string StudentId { get; set; }
    }
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentResponse?>
    {
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(ApiService apiService, IMapper mapper)
        {

            _apiService = apiService;
            _mapper = mapper;
        }
        public async Task<GetStudentResponse?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string endpoint = $"api/v1/student/{request.StudentId}";
                string jsonResponse = await _apiService.SendAsync(endpoint, request.GetTokenResponse);
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    GetStudentResponse student = JsonConvert.DeserializeObject<GetStudentResponse>(responseData.Data.ToString())!;
                    return student;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra ngoại lệ: {ex.Message}");
                throw;
            }
        }
    }
}

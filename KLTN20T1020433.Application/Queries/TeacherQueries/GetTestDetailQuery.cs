using AutoMapper;
using KLTN20T1020433.Application.DTOs.TeacherDTOs;
using KLTN20T1020433.Domain.Teacher;
using KLTN20T1020433.Domain.Test;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries.TeacherQueries
{
    public class GetTestDetailQuery : IRequest<GetTestDetailResponse?>
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
    }
    public class GetTestDetailQueryHandler : IRequestHandler<GetTestDetailQuery, GetTestDetailResponse?>
    {
        private readonly ITestRepository _testDB;
        private readonly ITeacherRepository _teacherDB;
        private readonly IMapper _mapper;

        public GetTestDetailQueryHandler(ITestRepository testDB, ITeacherRepository teacherDB, IMapper mapper)
        {
            _testDB = testDB;
            _teacherDB = teacherDB;
            _mapper = mapper;
        }

        public async Task<GetTestDetailResponse?> Handle(GetTestDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var test = await _testDB.GetById(request.Id);
                if (test != null)
                {
                    if (request.TeacherId == test.TeacherId)
                    {
                        var testResponse = _mapper.Map<GetTestDetailResponse>(test);
                        var teacher = await _teacherDB.GetTeacherById(request.TeacherId);
                        testResponse.TeacherName = teacher.TeacherName;                        
                        return testResponse;
                    }
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

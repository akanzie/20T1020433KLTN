using KLTN20T1020433.Domain.Teacher;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Application.Queries
{
    public class GetTeacherNameQuery : IRequest<string>
    {
        public string TeacherId { get; set; }
    }
    public class GetTeacherNameQueryHandler : IRequestHandler<GetTeacherNameQuery, string>
    {
        private readonly ITeacherRepository _teacherDB;
        public GetTeacherNameQueryHandler(ITeacherRepository teacherDB)
        {
            _teacherDB = teacherDB;
        }
        public async Task<string> Handle(GetTeacherNameQuery request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _teacherDB.GetTeacherById(request.TeacherId);
            return teacher.TeacherName;

        }
    }
}

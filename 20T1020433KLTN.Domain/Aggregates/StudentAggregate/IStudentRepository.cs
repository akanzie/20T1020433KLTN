using _20T1020433KLTN.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020433KLTN.Domain.Aggregates.StudentAggregate
{
    public interface IStudentRepository
    {
        Task<Student> GetByUsername(string username);
    }
}

using Dapper;
using KLTN20T1020433.Domain.Student;
using KLTN20T1020433.Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class StudentRepository : _BaseRepository, IStudentRepository
    {
        public StudentRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> Add(Student student)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TeacherId = student.StudentId,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Email = student.Email
                    };

                    await connection.ExecuteAsync(
                        "AddStudent",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm giáo viên: " + ex.Message);
                throw;
            }
        }
        public async Task<Student?> GetStudentById(string id)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        Student = id
                    };

                    var student = await connection.QueryFirstOrDefaultAsync<Student>(
                        "GetStudentById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy thông tin giáo viên theo ID: " + ex.Message);
                throw;
            }
        }
    }
}

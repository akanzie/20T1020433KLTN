using Dapper;
using KLTN20T1020433.Domain.Teacher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class TeacherRepository : _BaseRepository, ITeacherRepository
    {
        public TeacherRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> Add(Teacher teacher)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TeacherId = teacher.TeacherId,
                        TeacherName = teacher.TeacherName
                    };

                    await connection.ExecuteAsync(
                        "AddTeacher",
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
        public async Task<Teacher?> GetTeacherById(string id)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TeacherId = id
                    };

                    var teacher = await connection.QueryFirstOrDefaultAsync<Teacher>(
                        "GetTeacherById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return teacher;
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

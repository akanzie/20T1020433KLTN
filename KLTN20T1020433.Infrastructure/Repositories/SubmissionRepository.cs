using Dapper;
using KLTN20T1020433.Domain.Submission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN20T1020433.Infrastructure.Repositories
{
    public class SubmissionRepository : _BaseRepository, ISubmissionRepository
    {
        public SubmissionRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> Add(Submission data)
        {
            try
            {
                int id = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"INSERT INTO Submissions (StudentId, TestId, SubmittedTime, IPAddress, Status)
                VALUES (@StudentId, @TestId, @SubmittedTime, @IPAddress, @Status);
                SELECT SCOPE_IDENTITY()";
                    var parameters = new
                    {
                        StudentId = data.StudentId,
                        TestId = data.TestId,
                        SubmittedTime = data.SubmittedTime,
                        IPAddress = data.IPAddress,
                        Status = data.Status.ToString()
                    };
                    id = await connection.ExecuteScalarAsync<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"DELETE FROM Submissions WHERE SubmissionId = @SubmissionId";
                    var parameters = new
                    {
                        FileId = id
                    };
                    result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<Submission> GetById(int id)
        {
            try
            {
                Submission? submission = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM Submissions WHERE SubmissionId = @SubmissionId";
                    var parameters = new
                    {
                        SubmissionId = id
                    };
                    submission = await connection.QueryFirstOrDefaultAsync<Submission>(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return submission;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn bài nộp theo ID: " + ex.Message);
                throw;
            }
        }

        public async Task<Submission> GetByTestIdAndStudentId(int testId, string studentId)
        {
            try
            {
                Submission? submission = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM Submissions WHERE TestId = @TestId AND StudentId = @StudentId";
                    var parameters = new
                    {
                        TestId = testId,
                        StudentId = studentId
                    };
                    submission = await connection.QueryFirstOrDefaultAsync<Submission>(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return submission;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Submission>> GetSubmissionByTestId(int testId)
        {
            try
            {
                List<Submission> submissions = new List<Submission>();
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM Submissions WHERE TestId = @TestId";
                    var parameters = new
                    {
                        TestId = testId
                    };
                    submissions = (await connection.QueryAsync<Submission>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                }
                return submissions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn danh sách bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(Submission data)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"UPDATE Submissions
                SET SubmittedTime = @SubmittedTime, IPAddress = @IPAddress, Status = @Status
                WHERE SubmissionId = @SubmissionId";
                    var parameters = new
                    {
                        SubmittedTime = data.SubmittedTime,
                        IPAddress = data.IPAddress,
                        Status = data.Status.ToString(),
                        SubmissionId = data.SubmissionId
                    };
                    result = await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi cập nhật bài nộp: " + ex.Message);
                throw;
            }
        }
    }
}

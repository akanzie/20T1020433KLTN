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
                    var parameters = new
                    {
                        StudentId = data.StudentId,
                        TestId = data.TestId,
                        SubmittedTime = data.SubmittedTime,
                        IPAddress = data.IPAddress,
                        Status = data.Status.ToString()
                    };
                    id = await connection.ExecuteScalarAsync<int>(
                        "AddSubmission",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
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
                    var parameters = new
                    {
                        SubmissionId = id
                    };
                    result = await connection.ExecuteAsync(
                        "DeleteSubmission",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<Submission?> GetById(int id)
        {
            try
            {
                Submission? submission = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        SubmissionId = id
                    };
                    submission = await connection.QueryFirstOrDefaultAsync<Submission>(
                        "GetSubmissionById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                }
                return submission;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn bài nộp theo ID: " + ex.Message);
                throw;
            }
        }

        public async Task<Submission?> GetByTestIdAndStudentId(int testId, string studentId)
        {
            try
            {
                Submission? submission = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TestId = testId,
                        StudentId = studentId
                    };
                    submission = await connection.QueryFirstOrDefaultAsync<Submission>(
                        "GetSubmissionByTestIdAndStudentId",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
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
                    var parameters = new
                    {
                        TestId = testId
                    };
                    submissions = (await connection.QueryAsync<Submission>(
                        "GetSubmissionsByTestId",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    )).ToList();
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
                    var parameters = new
                    {
                        SubmissionId = data.SubmissionId,
                        SubmittedTime = data.SubmittedTime,
                        IPAddress = data.IPAddress,
                        Status = data.Status.ToString()
                    };
                    result = await connection.ExecuteAsync(
                        "UpdateSubmission",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ) > 0;
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

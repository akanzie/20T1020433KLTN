using Dapper;
using KLTN20T1020433.Domain.Submission;
using KLTN20T1020433.Domain.Test;
using System.Data;
using System.Net;

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
                    var parameters = new DynamicParameters();
                    parameters.Add("@StudentId", data.StudentId);
                    parameters.Add("@TestId", data.TestId);
                    parameters.Add("@SubmissionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "AddSubmission",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    id = parameters.Get<int>("@SubmissionId");
                }
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm bài nộp: " + ex.Message);
                throw;
            }
        }        

        public async Task<int> CountSubmissions(int testId = 0, string searchValue = "", string statuses = "")
        {
            try
            {
                int count = 0;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TestId = testId,
                        SearchValue = searchValue ?? "",
                        SubmissionStatus = statuses ?? ""

                    };
                    count = await connection.ExecuteScalarAsync<int>(
                        "CountSubmissions", parameters, commandType: CommandType.StoredProcedure);
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi đếm số lượng bài kiểm tra của kỳ thi: " + ex.Message);
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

        public async Task<IEnumerable<Submission>> GetSubmissionsBySearch(int page = 1, int pageSize = 0, int testId = 0, string searchValue = "", string statuses = "")
        {
            try
            {
                List<Submission> listSubmissions = new List<Submission>();
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        Page = page,
                        PageSize = pageSize,
                        TestId = testId,
                        SearchValue = searchValue ?? "",
                        SubmissionStatus = statuses ?? "",
                    };
                    var result = await connection.QueryAsync<Submission>(
                        "GetSubmissions",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    listSubmissions = result.ToList();
                }
                return listSubmissions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy danh sách bài nộp của kỳ thi: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByTestId(int testId)
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
                        SubmitTime = data.SubmitTime,
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

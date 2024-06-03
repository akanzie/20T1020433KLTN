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
    public class SubmissionHistoryRepository : _BaseRepository, ISubmissionHistoryRepository
    {
        public SubmissionHistoryRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> Add(SubmissionHistory data)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        SubmissionId = data.SubmissionId,
                        SubmitTime = data.SubmitTime,
                        IPAddress = data.IPAddress
                    };

                    int id = await connection.ExecuteScalarAsync<int>(
                        "AddSubmissionHistory", parameters, commandType: CommandType.StoredProcedure);
                    return id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm lịch sử nộp bài: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckIPAddressExists(string iPAddress, int submissionId, int testId)
        {
            try
            {
                bool exists = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        TestId = testId,
                        SubmissionId = submissionId,
                        IPAddress = iPAddress,
                    };

                    exists = await connection.ExecuteScalarAsync<bool>(
                         "CheckIPAddressExists", parameters, commandType: CommandType.StoredProcedure);
                }
                return exists;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi kiểm tra địa chỉ IP tồn tại: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new { Id = id };
                    int affectedRows = await connection.ExecuteAsync(
                        "DeleteSubmissionHistory", parameters, commandType: CommandType.StoredProcedure);
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa lịch sử nộp bài: " + ex.Message);
                throw;
            }
        }

        public async Task<SubmissionHistory?> GetById(int id)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new { Id = id };
                    var result = await connection.QuerySingleOrDefaultAsync<SubmissionHistory>(
                        "GetSubmissionHistoryById", parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy thông tin lịch sử nộp bài: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SubmissionHistory>> GetHistorysBySubmissionId(int submissionId)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new { SubmissionId = submissionId };
                    var results = await connection.QueryAsync<SubmissionHistory>(
                        "GetHistorysBySubmissionId", parameters, commandType: CommandType.StoredProcedure);
                    return results;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi lấy danh sách lịch sử nộp bài: " + ex.Message);
                throw;
            }
        }
    }
}

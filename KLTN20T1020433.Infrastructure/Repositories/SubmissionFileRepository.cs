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
    public class SubmissionFileRepository : _BaseRepository, ISubmissionFileRepository
    {
        public SubmissionFileRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> Add(SubmissionFile file)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = file.FileId,
                        FileName = file.FileName ?? "",
                        FilePath = file.FilePath ?? "",
                        MimeType = file.MimeType ?? "",
                        Size = file.Size,
                        SubmissionId = file.SubmissionId,
                        OriginalName = file.OriginalName ?? ""
                    };

                    // Call the stored procedure
                    result = await connection.ExecuteAsync("AddSubmissionFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi thêm tệp bài nộp: " + ex.Message);
                throw;
            }
        }
        public async Task<bool> CheckFileAuthorize(string studentId, Guid id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi kiểm tra quyền sở hữu tệp: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(Guid fileId)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var parameters = new
                    {
                        FileId = fileId
                    };

                    // Call the stored procedure
                    result = await connection.ExecuteAsync("DeleteSubmissionFile", parameters, commandType: CommandType.StoredProcedure) > 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi xóa tệp bài nộp: " + ex.Message);
                throw;
            }
        }
        public async Task<IEnumerable<SubmissionFile>> GetFileBySubmissionId(int submissionId)
        {
            try
            {
                List<SubmissionFile> files = new List<SubmissionFile>();
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM SubmissionFiles WHERE SubmissionId = @SubmissionId";
                    var parameters = new
                    {
                        SubmissionId = submissionId
                    };
                    files = (await connection.QueryAsync<SubmissionFile>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                }
                return files;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn các tệp của bài nộp: " + ex.Message);
                throw;
            }
        }

        public async Task<SubmissionFile?> GetById(Guid fileId)
        {
            try
            {
                SubmissionFile? file = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM SubmissionFiles WHERE FileId = @FileId";
                    var parameters = new
                    {
                        FileId = fileId
                    };
                    file = await connection.QueryFirstOrDefaultAsync<SubmissionFile>(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return file;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi truy vấn tệp bài nộp theo ID: " + ex.Message);
                throw;
            }
        }
    }
}

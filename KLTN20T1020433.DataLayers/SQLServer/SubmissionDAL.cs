using System;
using System.Net;
using System.Numerics;
using KLTN20T1020433.DomainModels.Entities;
using KLTN20T1020433.DomainModels.Interfaces;
using Dapper;
using System.Data;

namespace KLTN20T1020433.DataLayers.SQLServer
{
    public class SubmissionDAL : _BaseDAL, ISubmissionDAL
    {
        public SubmissionDAL(string connectionString) : base(connectionString)
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

        public async Task<Guid> AddSubmissionFile(SubmissionFile file)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"INSERT INTO SubmissionFiles (FileId, SubmissionId, FileName, FilePath, MimeType, Size, OriginalName)
                VALUES (@FileId, @SubmissionId, @FileName, @FilePath, @MimeType, @Size, @OriginalName)";
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
                    await connection.ExecuteAsync(sql: sql, param: parameters, commandType: CommandType.Text);
                }
                return file.FileId;
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

        public async Task<bool> DeleteSubmissionFile(Guid fileId)
        {
            try
            {
                bool result = false;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"DELETE FROM SubmissionFiles WHERE FileId = @FileId";
                    var parameters = new
                    {
                        FileId = fileId
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

        public async Task<Submission?> Get(int testId, string studentId)
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

        public async Task<Submission?> GetById(int submissionId)
        {
            try
            {
                Submission? submission = null;
                using (var connection = await OpenConnectionAsync())
                {
                    var sql = @"SELECT * FROM Submissions WHERE SubmissionId = @SubmissionId";
                    var parameters = new
                    {
                        SubmissionId = submissionId
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

        public async Task<IList<SubmissionFile>> GetFilesOfSubmission(int submissionId)
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

        public async Task<SubmissionFile?> GetSubmissionFile(Guid fileId)
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

        public async Task<IList<Submission>> GetSubmissions(int testId)
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
